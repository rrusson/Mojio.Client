﻿using Mojio;
using Mojio.Events;
//using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mojio.Client
{
    public partial class MockMojioClient
    {
        /// <summary>
        /// Login to mojio using a valid facebook access_token
        /// </summary>
        /// <param name="access_token">Facebook access_token</param>
        /// <returns></returns>
        public Token FacebookLogin(string access_token)
        {
            string message;
            HttpStatusCode code;

            return FacebookLogin(access_token, out code, out message);
        }

        /// <summary>
        /// Login to mojio using a valid facebook access_token
        /// </summary>
        /// <param name="access_token">Facebook access_token</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public Token FacebookLogin(string access_token, out HttpStatusCode code)
        {
            string message;

            return FacebookLogin(access_token, out code, out message);
        }

        /// <summary>
        /// Login to mojio using a valid facebook access_token
        /// </summary>
        /// <param name="access_token">Facebook access_token</param>
        /// <param name="message">Http response content</param>
        /// <returns></returns>
        public Token FacebookLogin(string access_token, out string message)
        {
            HttpStatusCode code;

            return FacebookLogin(access_token, out code, out message);
        }

        /// <summary>
        /// Login to mojio using a valid facebook access_token
        /// </summary>
        /// <param name="access_token">Facebook access_token</param>
        /// <param name="message">Http response content</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public Token FacebookLogin(string access_token, out HttpStatusCode code, out string message)
        {
            var task = FacebookLoginAsync(access_token);
            task.RunSynchronously();

            var response = task.Result;

            message = response.Content;
            code = response.StatusCode;

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return response.Data;
        }

        /// <summary>
        /// Login to mojio using a valid facebook access_token
        /// </summary>
        /// <returns>Request response</returns>
        /// <param name="access_token">Facebook access_token.</param>
        public Task<MojioResponse<Token>> FacebookLoginAsync(string access_token)
        {
            //if (Token == null)
            //    throw new Exception("Valid session must be initialized first."); // Can only "Login" if already authenticated app.

            //var request = GetRequest(Request("login", "facebook", "setexternaluser"), Method.GET);

            ////request.AddParameter("userOrEmail", userOrEmail);
            //request.AddParameter("accessToken", access_token);

            //var task = RequestAsync<Token>(request);
            //return task.ContinueWith<MojioResponse<Token>>(r =>
            //{
            //    var response = r.Result;
            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        Token = response.Data;
            //        ResetCurrentUser();
            //    }

            //    return response;
            //});
            return null;
        }

        /// <summary>
        /// Register a new user with Mojio.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="email">Email address</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public User RegisterUser(string username, string email, string password)
        {
            string message;
            HttpStatusCode code;
            if (User != null)
            {
                if (User.UserName == username.ToLower() || User.Email == email.ToLower())
                    return null;
            }
            return RegisterUser(username, email, password, out code, out message);
        }

        /// <summary>
        /// Register a new user with Mojio.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="email">Email address</param>
        /// <param name="password">Password</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public User RegisterUser(string username, string email, string password, out HttpStatusCode code)
        {
            string message;
            return RegisterUser(username, email, password, out code, out message);
        }

        /// <summary>
        /// Register a new user with Mojio.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="email">Email address</param>
        /// <param name="password">Password</param>
        /// <param name="message">Http response content</param>
        /// <returns></returns>
        public User RegisterUser(string username, string email, string password, out string message)
        {
            HttpStatusCode code;
            return RegisterUser(username, email, password, out code, out message);
        }

        /// <summary>
        /// Register a new user with Mojio.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="email">Email address</param>
        /// <param name="password">Password</param>
        /// <param name="message">Http response content</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public User RegisterUser(string username, string email, string password, out HttpStatusCode code, out string message)
        {
            var task = RegisterUserAsync(username, email, password);

            var response = task.Result;

            message = response.Content;
            code = response.StatusCode;

            if (response.StatusCode != HttpStatusCode.Created)
                return null;

            return response.Data;
        }

        /// <summary>
        /// Async Register a new user with Mojio.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="email">Email address</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public Task<MojioResponse<User>> RegisterUserAsync(string username, string email, string password)
        {
            LoadMockUser();
            
            MojioResponse<User> m;
            if(email.Contains('@'))
            {
                User.UserName = username;
                User.Email = email;
                m = new MojioResponse<User> 
                { 
                Content="User Data",
                Data=User,
                StatusCode=HttpStatusCode.Created
                };
            }
            else
            {
                m = new MojioResponse<User>
                {
                    Content = "Null",
                    Data = null,
                    StatusCode = HttpStatusCode.Forbidden
                };
            }
            var task = RequestAsync<User>(m);
            return task;
            
        }

        User _currentUser;
        public User CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return User;
                if (Token.UserId != null)
                    _currentUser = Get<User>(Token.UserId.Value);
                return User;
            }
        }

        /// <summary>
        /// Check if there is a logged in user.
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn()
        {
            return Token.UserId != null;
        }

        void ResetCurrentUser()
        {
            _currentUser = null;
        }

        /// <summary>
        /// Change current user's password.
        /// </summary>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <param name="message">Http response content</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword, out HttpStatusCode code, out string message)
        {

            
            if ( changePassword==null  )
            {
                changePassword = new ChangePassword
                {
                    NewPassword = newPassword,
                    OldPassword = oldPassword
                };
                code = HttpStatusCode.OK;
                message = "Password Changed Successfully.";
                return true;
            }
            else if (changePassword.NewPassword != newPassword)
            {
                changePassword = new ChangePassword
                {
                    NewPassword = newPassword,
                    OldPassword = oldPassword
                };
                code = HttpStatusCode.OK;
                message = "Password Changed Successfully.";
                return true;
            }
            else
            {
                code = HttpStatusCode.Forbidden;
                message = "Password Not Changed.";
                return false;
            }

        }

        /// <summary>
        /// Change current user's password.
        /// </summary>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            HttpStatusCode code;
            string message;
            return ChangePassword(oldPassword, newPassword, out code, out message);
        }

        /// <summary>
        /// Change current user's password.
        /// </summary>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <param name="message">Http response content</param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword, out string message)
        {
            HttpStatusCode code;
            return ChangePassword(oldPassword, newPassword, out code, out message);
        }

        /// <summary>
        /// Change current user's password.
        /// </summary>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword, out HttpStatusCode code)
        {
            string message;
            return ChangePassword(oldPassword, newPassword, out code, out message);
        }

        /// <summary>
        /// Send a password reset request to user's email.
        /// </summary>
        /// <param name="oldPassword">User's name or email</param>
        /// <param name="newPassword">Return URL</param>
        /// <param name="message">Http response content</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public bool RequestPasswordReset(string userNameOrEmail, string returnUrl, out HttpStatusCode code, out string message)
        {
            //string action = Map[typeof(User)];
            //var request = GetRequest(Request(action, userNameOrEmail, "ResetPassword"), Method.POST);
            //request.AddBody(returnUrl);

            //var response = RestClient.Execute(request);
            //code = response.StatusCode;
            //message = response.Content;

            //if (response.StatusCode != HttpStatusCode.OK)
            //{
            //    ThrowError(response.Content);
            //}
            code = HttpStatusCode.OK;
            message = "Password reset successfully.";
            return true;
        }

        /// <summary>
        /// Send a password reset request to user's email.
        /// </summary>
        /// <param name="oldPassword">User's name or email</param>
        /// <param name="newPassword">Return URL</param>
        /// <returns></returns>
        public bool RequestPasswordReset(string userNameOrEmail, string returnUrl)
        {
            HttpStatusCode code;
            string message;
            return RequestPasswordReset(userNameOrEmail, returnUrl, out code, out message);
        }

        /// <summary>
        /// Send a password reset request to user's email.
        /// </summary>
        /// <param name="oldPassword">User's name or email</param>
        /// <param name="newPassword">Return URL</param>
        /// <param name="message">Http response content</param>
        /// <returns></returns>
        public bool RequestPasswordReset(string userNameOrEmail, string returnUrl, out string message)
        {
            HttpStatusCode code;
            return RequestPasswordReset(userNameOrEmail, returnUrl, out code, out message);
        }

        /// <summary>
        /// Send a password reset request to user's email.
        /// </summary>
        /// <param name="oldPassword">User's name or email</param>
        /// <param name="newPassword">Return URL</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public bool RequestPasswordReset(string userNameOrEmail, string returnUrl, out HttpStatusCode code)
        {
            string message;
            return RequestPasswordReset(userNameOrEmail, returnUrl, out code, out message);
        }

        /// <summary>
        /// Reset a users password using a reset token.
        /// </summary>
        /// <param name="reset">Reset request</param>
        /// <param name="message">Http response content</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public bool PasswordReset(ResetPassword reset, out HttpStatusCode code, out string message)
        {
            //string action = Map[typeof(User)];
            //var request = GetRequest(Request(action, reset.UserNameOrEmail, "ResetPassword"), Method.PUT);
            //request.AddBody(reset);

            //var response = RestClient.Execute(request);
            //code = response.StatusCode;
            //message = response.Content;

            //if (response.StatusCode != HttpStatusCode.OK)
            //    return false;
            code = HttpStatusCode.OK;
            message = "";

            return true;
        }

        /// <summary>
        /// Reset a users password using a reset token.
        /// </summary>
        /// <param name="reset">Reset request</param>
        /// <returns></returns>
        public bool PasswordReset(ResetPassword reset)
        {
            HttpStatusCode code;
            string message;
            return PasswordReset(reset, out code, out message);
        }

        /// <summary>
        /// Reset a users password using a reset token.
        /// </summary>
        /// <param name="reset">Reset request</param>
        /// <param name="message">Http response content</param>
        /// <returns></returns>
        public bool PasswordReset(ResetPassword reset, out string message)
        {
            HttpStatusCode code;
            return PasswordReset(reset, out code, out message);
        }

        /// <summary>
        /// Reset a users password using a reset token.
        /// </summary>
        /// <param name="reset">Reset request</param>
        /// <param name="code">Http response status code</param>
        /// <returns></returns>
        public bool PasswordReset(ResetPassword reset, out HttpStatusCode code)
        {
            string message;
            return PasswordReset(reset, out code, out message);
        }

        /// <summary>
        /// Get a collection of applictions owned by a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public Results<App> UserApps(Guid userId, int page = 1)
        {
            //return GetBy<App, User>(userId, page);
            
            Apps.Add(App);
            AppsResult = new Results<App>
            {
                Data = Apps,
                TotalRows=Apps.Count
            };
            return AppsResult;
        }

        /// <summary>
        /// Get a collection of mojio devices owned by a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public Results<Device> UserMojios(Guid userId, int page = 1)
        {
            //return GetBy<Device, User>(userId, page);
            //Devices.Add(Device);
            DevicesResult = new Results<Device> { 
             Data=Devices.AsEnumerable(),
             TotalRows=Devices.Count()
            };
            return DevicesResult;
        }

        /// <summary>
        /// Get a collection of trips owned by a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public Results<Trip> UserTrips(Guid userId, int page = 1)
        {
            //return GetBy<Trip, User>(userId, page);
            TripsResult = new Results<Trip>
            {
                Data = Trips.AsEnumerable(),
                TotalRows = Trips.Count
            };
            return TripsResult;
        }

        /// <summary>
        /// Get a collection of events owned by a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public Results<Event> UserEvents(Guid userId, int page = 1)
        {
           return GetBy<Event, User>(userId, page);
            //return EventsResult;
        }

        public Address GetShipping(Guid? userId = null)
        {
           
            return Address;
        }

        public bool SaveShipping(Address shipping, Guid? userId = null)
        {
            //if (userId == null)
            //    userId = CurrentUser.Id;

            //string action = Map[typeof(User)];
            //var request = GetRequest(Request(action, userId, "shipping"), Method.POST);
            //request.AddBody(shipping);

            //var response = RestClient.Execute(request);
            //return response.StatusCode == HttpStatusCode.OK;
            Address = shipping;
            if (shipping != null)
                return true;
            else
                return false;
        }

        public CreditCard GetCreditCard(Guid? userId = null)
        {
            
            return CreditCard;
        }

        public bool SaveCreditCard(CreditCard creditCard, out string message, Guid? userId = null)
        {
            HttpStatusCode code;
            CreditCard = creditCard;
            return SaveCreditCard(creditCard, out code, out message, userId);
        }

        public bool SaveCreditCard(CreditCard creditCard, out HttpStatusCode code, Guid? userId = null)
        {
            string message;
            
            return SaveCreditCard(creditCard, out code, out message, userId);
        }

        public bool SaveCreditCard(CreditCard creditCard, Guid? userId = null)
        {
            HttpStatusCode code;
            string message;
            CreditCard = creditCard;
            return SaveCreditCard(CreditCard, out code, out message, userId);
        }

        public bool SaveCreditCard(CreditCard creditCard, out HttpStatusCode code, out string message, Guid? userId = null)
        {
            //if (userId == null)
            //    userId = CurrentUser.Id;

            //string action = Map[typeof(User)];
            //var request = GetRequest(Request(action, userId, "creditcard"), Method.POST);
            //request.AddBody(creditCard);

            //var response = RestClient.Execute<bool>(request);
            //code = response.StatusCode;
            //message = response.Content;

            //return response.Data;
            
            if (creditCard != null)
            {
                code = HttpStatusCode.OK;
                message = "Saved";
                return true;
            }
            else

            {
                code = HttpStatusCode.BadRequest;
                message = "Not Saved";
                return false; 
            }
        }

        public bool SetImage(byte[] data, string mimetype, out HttpStatusCode code, out string message, Guid? userId = null)
        {
            UserImage = data;
            code = HttpStatusCode.OK;
            message = "";
            return true;
        }

        public bool DeleteImage(out HttpStatusCode code, out string message, Guid? userId = null)
        {
            UserImage = null;
            code = HttpStatusCode.OK;
            message = "";
            return true;
        }

        public byte[] GetImage(ImageSize size = ImageSize.Small, Guid? userId = null)
        {
            return UserImage;
       }
    }
}