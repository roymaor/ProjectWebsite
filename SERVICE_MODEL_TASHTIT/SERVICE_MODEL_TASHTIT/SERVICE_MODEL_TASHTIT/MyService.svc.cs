using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MODEL;

namespace SERVICE_MODEL_TASHTIT
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
    public class MyService : IMyService
    {

        #region CITIES
        public bool AddCity(City city)
        {
            Cities cities = new Cities();
            cities.SelectAll();
            if (cities.Add(city))
                return cities.Save();
            else
                return false;
        }

        public bool DeleteCity(City city)
        {
            Cities cities = new Cities();
            cities.SelectAll();
            cities.Delete(city);
            return cities.Save();
        }

        public bool ModifyCity(City oldCity, City newCity)
        {
            Cities cities = new Cities();
            cities.SelectAll();
            if (cities.Modify(oldCity, newCity))
                return cities.Save();
            else
                return false;
        }

        public Cities SelectAllCities()
        {
            Cities cities = new Cities();
            cities.SelectAll();
            return cities;
        }
        #endregion CITIES

        /// //////////////////////////////////////////////////////////////////////////////// /////////////////////////////////////////////////////////////////////////////
        /// 
        #region AREACODES
        public AreaCodes SelectAllAreaCodes()
        {
            AreaCodes areaCodes = new AreaCodes();
            areaCodes.SelectAll();
            return areaCodes;
        }
        public bool AddAreaCode(AreaCode areaCode)
        {
            AreaCodes areaCodes = new AreaCodes();
            areaCodes.SelectAll();
            if (areaCodes.Add(areaCode))
                return areaCodes.Save();
            else
                return false;
        }

        public bool DeleteAreaCode(AreaCode areaCode)
        {
            AreaCodes areaCodes = new AreaCodes();
            areaCodes.SelectAll();
            areaCodes.Delete(areaCode);
            return areaCodes.Save();
        }

        public bool ModifyAreaCode(AreaCode oldAreaCode, AreaCode newAreaCode)
        {
            AreaCodes areaCodes = new AreaCodes();
            areaCodes.SelectAll();
            if (areaCodes.Modify(oldAreaCode, newAreaCode))
                return areaCodes.Save();
            else
                return false;
        }

        #endregion AREACODES

        /// /////////////////////////////////////////////////////////////////////////////
        /// 
      #region BRANDS 

        public Brands SelectAllBrands()
        {
            Brands brands = new Brands();
            brands.SelectAll();
            return brands;
        }

        public bool AddBrand(Brand brand)
        {
            Brands brands = new Brands();
            brands.SelectAll();
            if (brands.Add(brand))
                return brands.Save();
            else
                return false;
        }

        public bool ModifyBrand(Brand oldBrand, Brand newBrand)
        {
            Brands brands = new Brands();
            brands.SelectAll();
            if (brands.Modify(oldBrand, newBrand))
                return brands.Save();
            else
                return false;
        }

        public bool DeleteBrand(Brand brand)
        {
            Brands brands = new Brands();
            brands.SelectAll();
            brands.Delete(brand);
            return brands.Save();
        }
        #endregion BRANDS

        /// /////////////////////////////////////////////////////////////////////////////
        /// 
        #region CATEGORIES

        public Categories SelectAllCategories()
        {
            Categories categories = new Categories();
            categories.SelectAll();
            return categories;
        }

        public bool AddCategory(Category category)
        {
            Categories categories = new Categories();
            categories.SelectAll();
            if (categories.Add(category))
                return categories.Save();
            else
                return false;
        }

        public bool ModifyCategory(Category oldCategory, Category newCategory)
        {
            Categories categories = new Categories();
            categories.SelectAll();
            if (categories.Modify(oldCategory, newCategory))
                return categories.Save();
            else
                return false;
        }

        public bool DeleteCategory(Category category)
        {
            Categories categories = new Categories();
            categories.SelectAll();
            categories.Delete(category);
            return categories.Save();
        }
        #endregion CATEGORIES


        /// /////////////////////////////////////////////////////////////////////////////

        #region SIZES

        public Sizes SelectAllSizes()
        {
            Sizes sizes = new Sizes();
            sizes.SelectAll();
            return sizes;
        }

        public bool AddSize(Size size)
        {
            Sizes sizes = new Sizes();
            sizes.SelectAll();
            if (sizes.Add(size))
                return sizes.Save();
            else
                return false;
        }

        public bool ModifySize(Size oldSize, Size newSize)
        {
            Sizes sizes = new Sizes();
            sizes.SelectAll();
            if (sizes.Modify(oldSize, newSize))
                return sizes.Save();
            else
                return false;
        }

        public bool DeleteSize(Size size)
        {
            Sizes sizes = new Sizes();
            sizes.SelectAll();
            sizes.Delete(size);
            return sizes.Save();
        }
        #endregion SIZES


        #region ROLES

        public Roles SelectAllRoles()
        {
            Roles roles = new Roles();
            roles.SelectAll();
            return roles;
        }

        public bool AddRole(Role role)
        {
            Roles roles = new Roles();
            roles.SelectAll();
            if (roles.Add(role))
                return roles.Save();
            else
                return false;
        }

        public bool ModifyRole(Role oldRole, Role newRole)
        {
            Roles roles = new Roles();
            roles.SelectAll();
            if (roles.Modify(oldRole, newRole))
                return roles.Save();
            else
                return false;
        }

        public bool DeleteRole(Role role)
        {
            Roles roles = new Roles();
            roles.SelectAll();
            roles.Delete(role);
            return roles.Save();
        }
        #endregion ROLES

        #region MEMBERS
        public bool AddMember(Member member)
        {
            Members members = new Members();
            members.SelectAll();
            if (members.Add(member))
                return members.Save();
            else
                return false;
        }

        public bool DeleteMember(Member member)
        {
            Members members = new Members();
            members.SelectAll();
            members.Delete(member);
            return members.Save();
        }

        public bool ModifyMember(Member oldMember, Member newMember)
        {
            Members members = new Members();
            members.SelectAll();
            if (members.Modify(oldMember, newMember))
                return members.Save();
            else
                return false;
        }

        public Members SelectAllMembers()
        {
            Members members = new Members();
            members.SelectAll();
            return members;
        }
        #endregion CITIES

    }
}
