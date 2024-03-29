﻿using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.HelperClasses;

namespace Services.DataService
{
    public class UserDataService
    {
        private AppDbContext context;

        public UserDataService(AppDbContext _context)
        {
            context = _context;
        }
        public List<User> GetAll()
        {
            try
            {

                return context.Users.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<User> GetAllManagers()
        {
            try
            {

                return context.Users.Where(x => x.Role == "Manager").ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<User> GetAllContributors()
        {
            try
            {

                return context.Users.Where(x => x.Role == "Contributor").ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<User> GetAllActiveUsersByID(long id)
        {
            try
            {
                return context.Users.Where(d => d.Id != id).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateUser(User _user)
        {
            try
            {
                if(_user != null)

                {
                    User user = context.Users.FirstOrDefault(d => d.Id == _user.Id);
                    if (user != null)
                    {
                        user.EditedDate = DateTime.Now;
                        user.FirstName = _user.FirstName;
                        user.LastName = _user.LastName;
                        user.EmailAddress = _user.EmailAddress;
                        user.ContactNumber = _user.ContactNumber;                       
                        user.AddressOne = _user.AddressOne;                      
                        user.Role = _user.Role;
                        user.Password = Utilities.GetPasswordHash(_user.Password);                        
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
       
        public User GetUserByID(long id)
        {
            try
            {
                return context.Users.FirstOrDefault(d => d.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateUserWallPaperByUserId(long userid, string wallpaperName)
        {
            try
            {
                User user = new User();
                user = context.Users.SingleOrDefault(d => d.Id == userid);
                if (user != null)
                {
                    user.Wallpaper = wallpaperName;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateUserProfilePicture(long userid, string profileImageName)
        {
            try
            {
                User user = new User();
                user = context.Users.SingleOrDefault(d => d.Id == userid);
                if (user != null)
                {
                    user.ImagePath = profileImageName;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CreateNewUser(User user)
        {
            bool isUserAddTrue = false;
            user.CreatedBy = -1;
            user.CreatedDate = DateTime.Now;
            user.Password = Utilities.GetPasswordHash(user.Password);
            
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                isUserAddTrue = true;
                return isUserAddTrue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckUserExistOrnotByEmailAddress(string email, long Id)
        {
            try
            {
                int count = 0;
                if(Id>0)
                {
                    count = context.Users.Where(x => x.EmailAddress.Trim().ToLower() == email.Trim().ToLower() && x.Id != Id).Count();
                }
                else
                {
                    count = context.Users.Where(x => x.EmailAddress.Trim().ToLower() == email.Trim().ToLower()).Count();
                }
               
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Delete(long id)
        {
            if (id > 0)
            {
                User user = context.Users.FirstOrDefault(d => d.Id == id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
