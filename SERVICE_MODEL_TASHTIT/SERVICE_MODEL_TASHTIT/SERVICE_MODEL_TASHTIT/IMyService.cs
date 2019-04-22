using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MODEL;

namespace SERVICE_MODEL_TASHTIT
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyService" in both code and config file together.
    [ServiceContract]
    public interface IMyService
    {
        #region CITIES
        [OperationContract]
        Cities SelectAllCities();

        [OperationContract]
        bool AddCity(City city);

        [OperationContract]
        bool ModifyCity(City oldCity, City newCity);

        [OperationContract]
        bool DeleteCity(City city);
        #endregion CITIES


        #region AREACODES
        [OperationContract]
        AreaCodes SelectAllAreaCodes();

        [OperationContract]
        bool AddAreaCode(AreaCode areaCode);

        [OperationContract]
        bool ModifyAreaCode(AreaCode oldAreaCode, AreaCode newAreaCode);

        [OperationContract]
        bool DeleteAreaCode(AreaCode areaCode);
        #endregion AREACODES

        #region BRANDS 
        
        [OperationContract]
        Brands SelectAllBrands();

        [OperationContract]
        bool AddBrand(Brand brand);

        [OperationContract]
        bool ModifyBrand(Brand oldBrand, Brand newBrand);

        [OperationContract]
        bool DeleteBrand(Brand brand);

        #endregion BRANDS

        #region CATEGORIES
        
        [OperationContract]
        Categories SelectAllCategories();

        [OperationContract]
        bool AddCategory(Category category);

        [OperationContract]
        bool ModifyCategory(Category oldCategory, Category newCategory);

        [OperationContract]
        bool DeleteCategory(Category category);
        #endregion CATEGORIES

        #region ROLES

        [OperationContract]
        Roles SelectAllRoles();

        [OperationContract]
        bool AddRole(Role role);

        [OperationContract]
        bool ModifyRole(Role OldRole, Role newRole);

        [OperationContract]
        bool DeleteRole(Role role);
        #endregion ROLES

        #region SIZES

        [OperationContract]
        Sizes SelectAllSizes();

        [OperationContract]
        bool AddSize(Size size);

        [OperationContract]
        bool ModifySize(Size OldSize, Size newSize);

        [OperationContract]
        bool DeleteSize(Size size);
        #endregion SIZES

        #region CITIES
        [OperationContract]
        Members SelectAllMembers();

        [OperationContract]
        bool AddMember(Member city);

        [OperationContract]
        bool ModifyMember(Member oldCity, Member newCity);

        [OperationContract]
        bool DeleteMember(Member city);
        #endregion CITIES

    }
}
