using Microsoft.VisualBasic;
using StoreRequisition.Class;
using StoreRequisition.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;

public class clsAuthenticate
{

    /// <summary>
    /// string specifying user name
    /// </summary>

    private string strUser;
    /// <summary>
    /// string specifying user password
    /// </summary>

    private string strPass;
    /// <summary>
    /// string specifying user domain
    /// </summary>

    private string strDomain;
    /// <summary>
    /// AuthenticationTypes specifying the security 
    /// protocol to use, i.e. Secure, SSL
    /// </summary>

    private AuthenticationTypes atAuthentType;
    //Private objSystemProfile As clsSystem

    /// <summary>
    /// default constructor
    /// </summary>
    public clsAuthenticate()
    {
        //objSystemProfile = New clsSystem
    }

    /// <summary>
    /// function that sets the domain name
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns>It returns true, if user passed 
    ///          something; otherwise, false</returns>
    public bool SetDomain(string strValue)
    {
        if (strValue.Length <= 0)
        {
            return false;
        }

        this.strDomain = "LDAP://" + strValue;

        return true;
    }

    /// <summary>
    /// function that sets user name
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns>It returns true, if user passed 
    ///          something; otherwise, false</returns>
    public bool SetUser(string strValue)
    {
        if (strValue.Length <= 0)
        {
            return false;
        }

        this.strUser = strValue;
        return true;
    }

    /// <summary>
    /// function that sets user password
    /// </summary>
    /// <param name="strValue"></param>
    /// <returns>It returns true, if user passed 
    ///          something; otherwise, false</returns>
    public bool SetPass(string strValue)
    {
        if (strValue.Length <= 0)
        {
            return false;
        }

        this.strPass = strValue;
        return true;
    }

    /// <summary>
    /// function that sets user authentication type
    /// </summary>
    /// <param name="bValue"></param>
    public void SetAuthenticationType(bool bValue)
    {
        // set ssl to true if true is found
        if (bValue)
        {
            atAuthentType = AuthenticationTypes.SecureSocketsLayer;
        }
        else
        {
            // otherwise set it to secure  
            atAuthentType = AuthenticationTypes.Secure;
        }
    }

    /// <summary>
    /// function that performs login task
    /// and welcomes user if they are verified
    /// </summary>
    public bool Login()
    {
        // now create the directory entry to establish connection
        using (DirectoryEntry deDirEntry = new DirectoryEntry(this.strDomain, this.strUser, this.strPass, this.atAuthentType))
        {
            DataTable dsTmp = null;

            // if user is verified then it will welcome then  

            try
            {

                //deDirEntry.Name
                // TODO: add your specific tasks here
                return true;
                //Throw New Exception("Welcome to '" & deDirEntry.Name & "'")
                //MessageBox.Show("Welcome to '" & deDirEntry.Name & "'")
            }
            catch (System.Exception exp)
            {
                return false;
                throw new Exception("Sorry, unable to verify your information");
                //MessageBox.Show("Sorry, unable to verify your information")
            }

        }
    }

    private string _path;

    private string _filterAttribute;
    private string _filterAttribute2;
    private string _filterAttribute3;


    public clsAuthenticate(string path)
    {
        _path = path;
    }

    public bool IsAuthenticated(string domain, string username, string pwd)
    {
        string domainAndUsername = (domain + "\\") + username;
        DirectoryEntry entry = new DirectoryEntry(this.strDomain, domainAndUsername, pwd);

        try
        {
            //Bind to the native AdsObject to force authentication.
            object obj = entry.NativeObject;

            DirectorySearcher search = new DirectorySearcher(entry);

            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("employeeID");
            search.PropertiesToLoad.Add("cn");
             
            SearchResult result = search.FindOne();

            if (result == null)
            {
                return false;
            }

            //Update the new path to the user in the directory.
            _path = result.Path;
            _filterAttribute = (string)result.Properties["cn"][0];


            //test
            //DirectoryEntry entry1 = new DirectoryEntry("LDAP://mmct_domain");
            //object obj1 = entry1.NativeObject;
            //DirectorySearcher search1 = new DirectorySearcher(entry1);
            //search1.Filter = "(employeeid=1A012590)";
            //search1.PropertiesToLoad.Add("employeeid");
            //search1.PropertiesToLoad.Add("cn");
            //SearchResult result1 = search1.FindOne();
            //var strDepartments = result1.Properties["employeeid"];
            //var strDepartments2 = result1.Properties["cn"][0];
            //test












            if (result.Properties["employeeID"].Count == 0)
            {
                if (_filterAttribute.Contains("store")|| _filterAttribute.Contains("AS400"))
                {
                    return true;
                }
            }
              
        }
        catch (Exception ex)
        {
            return false;
            //Throw New Exception("Error authenticating user. " & ex.Message)
        }

        return true;
    }
    public UserAD GetAuthenticated(string domain, string username, string pwd)
    {
        string domainAndUsername = (domain + "\\") + username;
        DirectoryEntry entry = new DirectoryEntry(this.strDomain, domainAndUsername, pwd);

        try
        {
            //Bind to the native AdsObject to force authentication.
            object obj = entry.NativeObject;

            DirectorySearcher search = new DirectorySearcher(entry);

            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("employeeID");
            search.PropertiesToLoad.Add("cn");
            search.PropertiesToLoad.Add("department");            

            SearchResult result = search.FindOne();

            //Update the new path to the user in the directory.
            var tx = result.Properties["employeeID"];


            _path = result.Path;
            _filterAttribute = result.Properties["cn"].Count != 0 ? (string)result.Properties["cn"][0] : "";
            _filterAttribute2 = result.Properties["employeeID"].Count == 0 ? "": (string)result.Properties["employeeID"][0];
            _filterAttribute3 = result.Properties["department"].Count != 0 ? (string)result.Properties["department"][0]: "";

            UserAD userAD = new UserAD(); 
           
                userAD = new UserAD()
                {
                    InitName = _filterAttribute,
                    EN = _filterAttribute2,
                    Departments = _filterAttribute3,
                    Authority = "General"                    
                };
            
            return userAD;
        }
        catch (Exception ex)
        {
            throw ex;
            //Throw New Exception("Error authenticating user. " & ex.Message)
        }
    }


    public UserAD getUserAD(string cn)
    {
        UserAD userAD = new UserAD();
        try
        {
            if (cn != "")
            { 

                DirectoryEntry entry = new DirectoryEntry("LDAP://mmct_domain");
                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(cn=" + cn + ")";

                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("employeeID");
                search.PropertiesToLoad.Add("department");
                SearchResult result = search.FindOne();
                _path = result.Path;
                _filterAttribute = result.Properties["cn"].Count != 0 ? (string)result.Properties["cn"][0] : "";
                _filterAttribute2 = result.Properties["employeeID"].Count == 0 ? "" : (string)result.Properties["employeeID"][0];
                _filterAttribute3 = result.Properties["department"].Count != 0 ? (string)result.Properties["department"][0] : "";

                userAD = new UserAD()
                {
                    InitName = _filterAttribute,
                    EN = _filterAttribute2,
                    Departments = _filterAttribute3
                };

            }



            return userAD;
        }
        catch (Exception)
        {

            throw;
        }




        


    }

    

}

