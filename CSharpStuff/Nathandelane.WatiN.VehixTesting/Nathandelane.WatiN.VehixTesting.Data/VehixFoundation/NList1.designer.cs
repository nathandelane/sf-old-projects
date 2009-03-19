﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nathandelane.WatiN.VehixTesting.Data.VehixFoundation
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="VehixFoundation")]
	public partial class VehixFoundationNListDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertNListTable(NListTable instance);
    partial void UpdateNListTable(NListTable instance);
    partial void DeleteNListTable(NListTable instance);
    partial void InsertNListCategoryTable(NListCategoryTable instance);
    partial void UpdateNListCategoryTable(NListCategoryTable instance);
    partial void DeleteNListCategoryTable(NListCategoryTable instance);
    #endregion
		
		public VehixFoundationNListDataContext() : 
				base(global::Nathandelane.WatiN.VehixTesting.Data.Properties.Settings.Default.VehixFoundationConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationNListDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationNListDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationNListDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationNListDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<NItemValueTable> NItemValueTables
		{
			get
			{
				return this.GetTable<NItemValueTable>();
			}
		}
		
		public System.Data.Linq.Table<NListTable> NListTables
		{
			get
			{
				return this.GetTable<NListTable>();
			}
		}
		
		public System.Data.Linq.Table<NListCategoryTable> NListCategoryTables
		{
			get
			{
				return this.GetTable<NListCategoryTable>();
			}
		}
		
		public System.Data.Linq.Table<NListColumnTable> NListColumnTables
		{
			get
			{
				return this.GetTable<NListColumnTable>();
			}
		}
		
		public System.Data.Linq.Table<NListItemTable> NListItemTables
		{
			get
			{
				return this.GetTable<NListItemTable>();
			}
		}
	}
	
	[Table(Name="NList.NItemValueTable")]
	public partial class NItemValueTable
	{
		
		private long _ID;
		
		private long _VFX_CreatorID;
		
		private long _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private long _ListItemID;
		
		private long _ListColumnID;
		
		private string _ItemColumnValue;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		public NItemValueTable()
		{
		}
		
		[Column(Storage="_ID", DbType="BigInt NOT NULL")]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt NOT NULL")]
		public long VFX_CreatorID
		{
			get
			{
				return this._VFX_CreatorID;
			}
			set
			{
				if ((this._VFX_CreatorID != value))
				{
					this._VFX_CreatorID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt NOT NULL")]
		public long VFX_UpdaterID
		{
			get
			{
				return this._VFX_UpdaterID;
			}
			set
			{
				if ((this._VFX_UpdaterID != value))
				{
					this._VFX_UpdaterID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DeletorID", DbType="BigInt")]
		public System.Nullable<long> VFX_DeletorID
		{
			get
			{
				return this._VFX_DeletorID;
			}
			set
			{
				if ((this._VFX_DeletorID != value))
				{
					this._VFX_DeletorID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateCreated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateCreated
		{
			get
			{
				return this._VFX_DateCreated;
			}
			set
			{
				if ((this._VFX_DateCreated != value))
				{
					this._VFX_DateCreated = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateUpdated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateUpdated
		{
			get
			{
				return this._VFX_DateUpdated;
			}
			set
			{
				if ((this._VFX_DateUpdated != value))
				{
					this._VFX_DateUpdated = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateDeleted", DbType="DateTime")]
		public System.Nullable<System.DateTime> VFX_DateDeleted
		{
			get
			{
				return this._VFX_DateDeleted;
			}
			set
			{
				if ((this._VFX_DateDeleted != value))
				{
					this._VFX_DateDeleted = value;
				}
			}
		}
		
		[Column(Storage="_ListItemID", DbType="BigInt NOT NULL")]
		public long ListItemID
		{
			get
			{
				return this._ListItemID;
			}
			set
			{
				if ((this._ListItemID != value))
				{
					this._ListItemID = value;
				}
			}
		}
		
		[Column(Storage="_ListColumnID", DbType="BigInt NOT NULL")]
		public long ListColumnID
		{
			get
			{
				return this._ListColumnID;
			}
			set
			{
				if ((this._ListColumnID != value))
				{
					this._ListColumnID = value;
				}
			}
		}
		
		[Column(Storage="_ItemColumnValue", DbType="NVarChar(128)")]
		public string ItemColumnValue
		{
			get
			{
				return this._ItemColumnValue;
			}
			set
			{
				if ((this._ItemColumnValue != value))
				{
					this._ItemColumnValue = value;
				}
			}
		}
		
		[Column(Storage="_ETL_NaturalKey", DbType="VarChar(256)")]
		public string ETL_NaturalKey
		{
			get
			{
				return this._ETL_NaturalKey;
			}
			set
			{
				if ((this._ETL_NaturalKey != value))
				{
					this._ETL_NaturalKey = value;
				}
			}
		}
		
		[Column(Storage="_ETL_Hash", DbType="Binary(16)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary ETL_Hash
		{
			get
			{
				return this._ETL_Hash;
			}
			set
			{
				if ((this._ETL_Hash != value))
				{
					this._ETL_Hash = value;
				}
			}
		}
		
		[Column(Storage="_ETL_JobCategory", DbType="VarChar(50)")]
		public string ETL_JobCategory
		{
			get
			{
				return this._ETL_JobCategory;
			}
			set
			{
				if ((this._ETL_JobCategory != value))
				{
					this._ETL_JobCategory = value;
				}
			}
		}
		
		[Column(Storage="_ETL_DateEffective", DbType="DateTime")]
		public System.Nullable<System.DateTime> ETL_DateEffective
		{
			get
			{
				return this._ETL_DateEffective;
			}
			set
			{
				if ((this._ETL_DateEffective != value))
				{
					this._ETL_DateEffective = value;
				}
			}
		}
	}
	
	[Table(Name="NList.NListTable")]
	public partial class NListTable : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private long _VFX_CreatorID;
		
		private long _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private string _Category;
		
		private string _Name;
		
		private string _Description;
		
		private System.Nullable<int> _MaxN;
		
		private string _Active;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		private EntityRef<NListCategoryTable> _NListCategoryTable;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(long value);
    partial void OnIDChanged();
    partial void OnVFX_CreatorIDChanging(long value);
    partial void OnVFX_CreatorIDChanged();
    partial void OnVFX_UpdaterIDChanging(long value);
    partial void OnVFX_UpdaterIDChanged();
    partial void OnVFX_DeletorIDChanging(System.Nullable<long> value);
    partial void OnVFX_DeletorIDChanged();
    partial void OnVFX_DateCreatedChanging(System.DateTime value);
    partial void OnVFX_DateCreatedChanged();
    partial void OnVFX_DateUpdatedChanging(System.DateTime value);
    partial void OnVFX_DateUpdatedChanged();
    partial void OnVFX_DateDeletedChanging(System.Nullable<System.DateTime> value);
    partial void OnVFX_DateDeletedChanged();
    partial void OnCategoryChanging(string value);
    partial void OnCategoryChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnMaxNChanging(System.Nullable<int> value);
    partial void OnMaxNChanged();
    partial void OnActiveChanging(string value);
    partial void OnActiveChanged();
    partial void OnETL_NaturalKeyChanging(string value);
    partial void OnETL_NaturalKeyChanged();
    partial void OnETL_HashChanging(System.Data.Linq.Binary value);
    partial void OnETL_HashChanged();
    partial void OnETL_JobCategoryChanging(string value);
    partial void OnETL_JobCategoryChanged();
    partial void OnETL_DateEffectiveChanging(System.Nullable<System.DateTime> value);
    partial void OnETL_DateEffectiveChanged();
    #endregion
		
		public NListTable()
		{
			this._NListCategoryTable = default(EntityRef<NListCategoryTable>);
			OnCreated();
		}
		
		[Column(Storage="_ID", DbType="BigInt NOT NULL")]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt NOT NULL")]
		public long VFX_CreatorID
		{
			get
			{
				return this._VFX_CreatorID;
			}
			set
			{
				if ((this._VFX_CreatorID != value))
				{
					this.OnVFX_CreatorIDChanging(value);
					this.SendPropertyChanging();
					this._VFX_CreatorID = value;
					this.SendPropertyChanged("VFX_CreatorID");
					this.OnVFX_CreatorIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt NOT NULL")]
		public long VFX_UpdaterID
		{
			get
			{
				return this._VFX_UpdaterID;
			}
			set
			{
				if ((this._VFX_UpdaterID != value))
				{
					this.OnVFX_UpdaterIDChanging(value);
					this.SendPropertyChanging();
					this._VFX_UpdaterID = value;
					this.SendPropertyChanged("VFX_UpdaterID");
					this.OnVFX_UpdaterIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DeletorID", DbType="BigInt")]
		public System.Nullable<long> VFX_DeletorID
		{
			get
			{
				return this._VFX_DeletorID;
			}
			set
			{
				if ((this._VFX_DeletorID != value))
				{
					this.OnVFX_DeletorIDChanging(value);
					this.SendPropertyChanging();
					this._VFX_DeletorID = value;
					this.SendPropertyChanged("VFX_DeletorID");
					this.OnVFX_DeletorIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DateCreated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateCreated
		{
			get
			{
				return this._VFX_DateCreated;
			}
			set
			{
				if ((this._VFX_DateCreated != value))
				{
					this.OnVFX_DateCreatedChanging(value);
					this.SendPropertyChanging();
					this._VFX_DateCreated = value;
					this.SendPropertyChanged("VFX_DateCreated");
					this.OnVFX_DateCreatedChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DateUpdated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateUpdated
		{
			get
			{
				return this._VFX_DateUpdated;
			}
			set
			{
				if ((this._VFX_DateUpdated != value))
				{
					this.OnVFX_DateUpdatedChanging(value);
					this.SendPropertyChanging();
					this._VFX_DateUpdated = value;
					this.SendPropertyChanged("VFX_DateUpdated");
					this.OnVFX_DateUpdatedChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DateDeleted", DbType="DateTime")]
		public System.Nullable<System.DateTime> VFX_DateDeleted
		{
			get
			{
				return this._VFX_DateDeleted;
			}
			set
			{
				if ((this._VFX_DateDeleted != value))
				{
					this.OnVFX_DateDeletedChanging(value);
					this.SendPropertyChanging();
					this._VFX_DateDeleted = value;
					this.SendPropertyChanged("VFX_DateDeleted");
					this.OnVFX_DateDeletedChanged();
				}
			}
		}
		
		[Column(Storage="_Category", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if ((this._Category != value))
				{
					if (this._NListCategoryTable.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCategoryChanging(value);
					this.SendPropertyChanging();
					this._Category = value;
					this.SendPropertyChanged("Category");
					this.OnCategoryChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="NVarChar(400)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_MaxN", DbType="Int")]
		public System.Nullable<int> MaxN
		{
			get
			{
				return this._MaxN;
			}
			set
			{
				if ((this._MaxN != value))
				{
					this.OnMaxNChanging(value);
					this.SendPropertyChanging();
					this._MaxN = value;
					this.SendPropertyChanged("MaxN");
					this.OnMaxNChanged();
				}
			}
		}
		
		[Column(Storage="_Active", DbType="Char(5) NOT NULL", CanBeNull=false)]
		public string Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				if ((this._Active != value))
				{
					this.OnActiveChanging(value);
					this.SendPropertyChanging();
					this._Active = value;
					this.SendPropertyChanged("Active");
					this.OnActiveChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_NaturalKey", DbType="VarChar(256)")]
		public string ETL_NaturalKey
		{
			get
			{
				return this._ETL_NaturalKey;
			}
			set
			{
				if ((this._ETL_NaturalKey != value))
				{
					this.OnETL_NaturalKeyChanging(value);
					this.SendPropertyChanging();
					this._ETL_NaturalKey = value;
					this.SendPropertyChanged("ETL_NaturalKey");
					this.OnETL_NaturalKeyChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_Hash", DbType="Binary(16)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary ETL_Hash
		{
			get
			{
				return this._ETL_Hash;
			}
			set
			{
				if ((this._ETL_Hash != value))
				{
					this.OnETL_HashChanging(value);
					this.SendPropertyChanging();
					this._ETL_Hash = value;
					this.SendPropertyChanged("ETL_Hash");
					this.OnETL_HashChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_JobCategory", DbType="VarChar(50)")]
		public string ETL_JobCategory
		{
			get
			{
				return this._ETL_JobCategory;
			}
			set
			{
				if ((this._ETL_JobCategory != value))
				{
					this.OnETL_JobCategoryChanging(value);
					this.SendPropertyChanging();
					this._ETL_JobCategory = value;
					this.SendPropertyChanged("ETL_JobCategory");
					this.OnETL_JobCategoryChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_DateEffective", DbType="DateTime")]
		public System.Nullable<System.DateTime> ETL_DateEffective
		{
			get
			{
				return this._ETL_DateEffective;
			}
			set
			{
				if ((this._ETL_DateEffective != value))
				{
					this.OnETL_DateEffectiveChanging(value);
					this.SendPropertyChanging();
					this._ETL_DateEffective = value;
					this.SendPropertyChanged("ETL_DateEffective");
					this.OnETL_DateEffectiveChanged();
				}
			}
		}
		
		[Association(Name="NListCategoryTable_NListTable", Storage="_NListCategoryTable", ThisKey="Category", OtherKey="Name", IsForeignKey=true)]
		public NListCategoryTable NListCategoryTable
		{
			get
			{
				return this._NListCategoryTable.Entity;
			}
			set
			{
				NListCategoryTable previousValue = this._NListCategoryTable.Entity;
				if (((previousValue != value) 
							|| (this._NListCategoryTable.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._NListCategoryTable.Entity = null;
						previousValue.NListTables.Remove(this);
					}
					this._NListCategoryTable.Entity = value;
					if ((value != null))
					{
						value.NListTables.Add(this);
						this._Category = value.Name;
					}
					else
					{
						this._Category = default(string);
					}
					this.SendPropertyChanged("NListCategoryTable");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="NList.NListCategoryTable")]
	public partial class NListCategoryTable : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private long _VFX_CreatorID;
		
		private long _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private string _Name;
		
		private string _Description;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		private EntitySet<NListTable> _NListTables;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(long value);
    partial void OnIDChanged();
    partial void OnVFX_CreatorIDChanging(long value);
    partial void OnVFX_CreatorIDChanged();
    partial void OnVFX_UpdaterIDChanging(long value);
    partial void OnVFX_UpdaterIDChanged();
    partial void OnVFX_DeletorIDChanging(System.Nullable<long> value);
    partial void OnVFX_DeletorIDChanged();
    partial void OnVFX_DateCreatedChanging(System.DateTime value);
    partial void OnVFX_DateCreatedChanged();
    partial void OnVFX_DateUpdatedChanging(System.DateTime value);
    partial void OnVFX_DateUpdatedChanged();
    partial void OnVFX_DateDeletedChanging(System.Nullable<System.DateTime> value);
    partial void OnVFX_DateDeletedChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnETL_NaturalKeyChanging(string value);
    partial void OnETL_NaturalKeyChanged();
    partial void OnETL_HashChanging(System.Data.Linq.Binary value);
    partial void OnETL_HashChanged();
    partial void OnETL_JobCategoryChanging(string value);
    partial void OnETL_JobCategoryChanged();
    partial void OnETL_DateEffectiveChanging(System.Nullable<System.DateTime> value);
    partial void OnETL_DateEffectiveChanged();
    #endregion
		
		public NListCategoryTable()
		{
			this._NListTables = new EntitySet<NListTable>(new Action<NListTable>(this.attach_NListTables), new Action<NListTable>(this.detach_NListTables));
			OnCreated();
		}
		
		[Column(Storage="_ID", DbType="BigInt NOT NULL")]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt NOT NULL")]
		public long VFX_CreatorID
		{
			get
			{
				return this._VFX_CreatorID;
			}
			set
			{
				if ((this._VFX_CreatorID != value))
				{
					this.OnVFX_CreatorIDChanging(value);
					this.SendPropertyChanging();
					this._VFX_CreatorID = value;
					this.SendPropertyChanged("VFX_CreatorID");
					this.OnVFX_CreatorIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt NOT NULL")]
		public long VFX_UpdaterID
		{
			get
			{
				return this._VFX_UpdaterID;
			}
			set
			{
				if ((this._VFX_UpdaterID != value))
				{
					this.OnVFX_UpdaterIDChanging(value);
					this.SendPropertyChanging();
					this._VFX_UpdaterID = value;
					this.SendPropertyChanged("VFX_UpdaterID");
					this.OnVFX_UpdaterIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DeletorID", DbType="BigInt")]
		public System.Nullable<long> VFX_DeletorID
		{
			get
			{
				return this._VFX_DeletorID;
			}
			set
			{
				if ((this._VFX_DeletorID != value))
				{
					this.OnVFX_DeletorIDChanging(value);
					this.SendPropertyChanging();
					this._VFX_DeletorID = value;
					this.SendPropertyChanged("VFX_DeletorID");
					this.OnVFX_DeletorIDChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DateCreated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateCreated
		{
			get
			{
				return this._VFX_DateCreated;
			}
			set
			{
				if ((this._VFX_DateCreated != value))
				{
					this.OnVFX_DateCreatedChanging(value);
					this.SendPropertyChanging();
					this._VFX_DateCreated = value;
					this.SendPropertyChanged("VFX_DateCreated");
					this.OnVFX_DateCreatedChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DateUpdated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateUpdated
		{
			get
			{
				return this._VFX_DateUpdated;
			}
			set
			{
				if ((this._VFX_DateUpdated != value))
				{
					this.OnVFX_DateUpdatedChanging(value);
					this.SendPropertyChanging();
					this._VFX_DateUpdated = value;
					this.SendPropertyChanged("VFX_DateUpdated");
					this.OnVFX_DateUpdatedChanged();
				}
			}
		}
		
		[Column(Storage="_VFX_DateDeleted", DbType="DateTime")]
		public System.Nullable<System.DateTime> VFX_DateDeleted
		{
			get
			{
				return this._VFX_DateDeleted;
			}
			set
			{
				if ((this._VFX_DateDeleted != value))
				{
					this.OnVFX_DateDeletedChanging(value);
					this.SendPropertyChanging();
					this._VFX_DateDeleted = value;
					this.SendPropertyChanged("VFX_DateDeleted");
					this.OnVFX_DateDeletedChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="NVarChar(400)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_NaturalKey", DbType="VarChar(256)")]
		public string ETL_NaturalKey
		{
			get
			{
				return this._ETL_NaturalKey;
			}
			set
			{
				if ((this._ETL_NaturalKey != value))
				{
					this.OnETL_NaturalKeyChanging(value);
					this.SendPropertyChanging();
					this._ETL_NaturalKey = value;
					this.SendPropertyChanged("ETL_NaturalKey");
					this.OnETL_NaturalKeyChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_Hash", DbType="Binary(16)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary ETL_Hash
		{
			get
			{
				return this._ETL_Hash;
			}
			set
			{
				if ((this._ETL_Hash != value))
				{
					this.OnETL_HashChanging(value);
					this.SendPropertyChanging();
					this._ETL_Hash = value;
					this.SendPropertyChanged("ETL_Hash");
					this.OnETL_HashChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_JobCategory", DbType="VarChar(50)")]
		public string ETL_JobCategory
		{
			get
			{
				return this._ETL_JobCategory;
			}
			set
			{
				if ((this._ETL_JobCategory != value))
				{
					this.OnETL_JobCategoryChanging(value);
					this.SendPropertyChanging();
					this._ETL_JobCategory = value;
					this.SendPropertyChanged("ETL_JobCategory");
					this.OnETL_JobCategoryChanged();
				}
			}
		}
		
		[Column(Storage="_ETL_DateEffective", DbType="DateTime")]
		public System.Nullable<System.DateTime> ETL_DateEffective
		{
			get
			{
				return this._ETL_DateEffective;
			}
			set
			{
				if ((this._ETL_DateEffective != value))
				{
					this.OnETL_DateEffectiveChanging(value);
					this.SendPropertyChanging();
					this._ETL_DateEffective = value;
					this.SendPropertyChanged("ETL_DateEffective");
					this.OnETL_DateEffectiveChanged();
				}
			}
		}
		
		[Association(Name="NListCategoryTable_NListTable", Storage="_NListTables", ThisKey="Name", OtherKey="Category")]
		public EntitySet<NListTable> NListTables
		{
			get
			{
				return this._NListTables;
			}
			set
			{
				this._NListTables.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_NListTables(NListTable entity)
		{
			this.SendPropertyChanging();
			entity.NListCategoryTable = this;
		}
		
		private void detach_NListTables(NListTable entity)
		{
			this.SendPropertyChanging();
			entity.NListCategoryTable = null;
		}
	}
	
	[Table(Name="NList.NListColumnTable")]
	public partial class NListColumnTable
	{
		
		private long _ID;
		
		private long _VFX_CreatorID;
		
		private long _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private long _ListID;
		
		private string _Attribute;
		
		private string _Header;
		
		private System.Nullable<int> _ItemRank;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		public NListColumnTable()
		{
		}
		
		[Column(Storage="_ID", DbType="BigInt NOT NULL")]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt NOT NULL")]
		public long VFX_CreatorID
		{
			get
			{
				return this._VFX_CreatorID;
			}
			set
			{
				if ((this._VFX_CreatorID != value))
				{
					this._VFX_CreatorID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt NOT NULL")]
		public long VFX_UpdaterID
		{
			get
			{
				return this._VFX_UpdaterID;
			}
			set
			{
				if ((this._VFX_UpdaterID != value))
				{
					this._VFX_UpdaterID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DeletorID", DbType="BigInt")]
		public System.Nullable<long> VFX_DeletorID
		{
			get
			{
				return this._VFX_DeletorID;
			}
			set
			{
				if ((this._VFX_DeletorID != value))
				{
					this._VFX_DeletorID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateCreated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateCreated
		{
			get
			{
				return this._VFX_DateCreated;
			}
			set
			{
				if ((this._VFX_DateCreated != value))
				{
					this._VFX_DateCreated = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateUpdated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateUpdated
		{
			get
			{
				return this._VFX_DateUpdated;
			}
			set
			{
				if ((this._VFX_DateUpdated != value))
				{
					this._VFX_DateUpdated = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateDeleted", DbType="DateTime")]
		public System.Nullable<System.DateTime> VFX_DateDeleted
		{
			get
			{
				return this._VFX_DateDeleted;
			}
			set
			{
				if ((this._VFX_DateDeleted != value))
				{
					this._VFX_DateDeleted = value;
				}
			}
		}
		
		[Column(Storage="_ListID", DbType="BigInt NOT NULL")]
		public long ListID
		{
			get
			{
				return this._ListID;
			}
			set
			{
				if ((this._ListID != value))
				{
					this._ListID = value;
				}
			}
		}
		
		[Column(Storage="_Attribute", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				if ((this._Attribute != value))
				{
					this._Attribute = value;
				}
			}
		}
		
		[Column(Storage="_Header", DbType="NVarChar(100)")]
		public string Header
		{
			get
			{
				return this._Header;
			}
			set
			{
				if ((this._Header != value))
				{
					this._Header = value;
				}
			}
		}
		
		[Column(Storage="_ItemRank", DbType="Int")]
		public System.Nullable<int> ItemRank
		{
			get
			{
				return this._ItemRank;
			}
			set
			{
				if ((this._ItemRank != value))
				{
					this._ItemRank = value;
				}
			}
		}
		
		[Column(Storage="_ETL_NaturalKey", DbType="VarChar(256)")]
		public string ETL_NaturalKey
		{
			get
			{
				return this._ETL_NaturalKey;
			}
			set
			{
				if ((this._ETL_NaturalKey != value))
				{
					this._ETL_NaturalKey = value;
				}
			}
		}
		
		[Column(Storage="_ETL_Hash", DbType="Binary(16)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary ETL_Hash
		{
			get
			{
				return this._ETL_Hash;
			}
			set
			{
				if ((this._ETL_Hash != value))
				{
					this._ETL_Hash = value;
				}
			}
		}
		
		[Column(Storage="_ETL_JobCategory", DbType="VarChar(50)")]
		public string ETL_JobCategory
		{
			get
			{
				return this._ETL_JobCategory;
			}
			set
			{
				if ((this._ETL_JobCategory != value))
				{
					this._ETL_JobCategory = value;
				}
			}
		}
		
		[Column(Storage="_ETL_DateEffective", DbType="DateTime")]
		public System.Nullable<System.DateTime> ETL_DateEffective
		{
			get
			{
				return this._ETL_DateEffective;
			}
			set
			{
				if ((this._ETL_DateEffective != value))
				{
					this._ETL_DateEffective = value;
				}
			}
		}
	}
	
	[Table(Name="NList.NListItemTable")]
	public partial class NListItemTable
	{
		
		private long _ID;
		
		private long _VFX_CreatorID;
		
		private long _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private long _ListID;
		
		private System.Nullable<int> _ItemRank;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		public NListItemTable()
		{
		}
		
		[Column(Storage="_ID", DbType="BigInt NOT NULL")]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt NOT NULL")]
		public long VFX_CreatorID
		{
			get
			{
				return this._VFX_CreatorID;
			}
			set
			{
				if ((this._VFX_CreatorID != value))
				{
					this._VFX_CreatorID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt NOT NULL")]
		public long VFX_UpdaterID
		{
			get
			{
				return this._VFX_UpdaterID;
			}
			set
			{
				if ((this._VFX_UpdaterID != value))
				{
					this._VFX_UpdaterID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DeletorID", DbType="BigInt")]
		public System.Nullable<long> VFX_DeletorID
		{
			get
			{
				return this._VFX_DeletorID;
			}
			set
			{
				if ((this._VFX_DeletorID != value))
				{
					this._VFX_DeletorID = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateCreated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateCreated
		{
			get
			{
				return this._VFX_DateCreated;
			}
			set
			{
				if ((this._VFX_DateCreated != value))
				{
					this._VFX_DateCreated = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateUpdated", DbType="DateTime NOT NULL")]
		public System.DateTime VFX_DateUpdated
		{
			get
			{
				return this._VFX_DateUpdated;
			}
			set
			{
				if ((this._VFX_DateUpdated != value))
				{
					this._VFX_DateUpdated = value;
				}
			}
		}
		
		[Column(Storage="_VFX_DateDeleted", DbType="DateTime")]
		public System.Nullable<System.DateTime> VFX_DateDeleted
		{
			get
			{
				return this._VFX_DateDeleted;
			}
			set
			{
				if ((this._VFX_DateDeleted != value))
				{
					this._VFX_DateDeleted = value;
				}
			}
		}
		
		[Column(Storage="_ListID", DbType="BigInt NOT NULL")]
		public long ListID
		{
			get
			{
				return this._ListID;
			}
			set
			{
				if ((this._ListID != value))
				{
					this._ListID = value;
				}
			}
		}
		
		[Column(Storage="_ItemRank", DbType="Int")]
		public System.Nullable<int> ItemRank
		{
			get
			{
				return this._ItemRank;
			}
			set
			{
				if ((this._ItemRank != value))
				{
					this._ItemRank = value;
				}
			}
		}
		
		[Column(Storage="_ETL_NaturalKey", DbType="VarChar(256)")]
		public string ETL_NaturalKey
		{
			get
			{
				return this._ETL_NaturalKey;
			}
			set
			{
				if ((this._ETL_NaturalKey != value))
				{
					this._ETL_NaturalKey = value;
				}
			}
		}
		
		[Column(Storage="_ETL_Hash", DbType="Binary(16)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary ETL_Hash
		{
			get
			{
				return this._ETL_Hash;
			}
			set
			{
				if ((this._ETL_Hash != value))
				{
					this._ETL_Hash = value;
				}
			}
		}
		
		[Column(Storage="_ETL_JobCategory", DbType="VarChar(50)")]
		public string ETL_JobCategory
		{
			get
			{
				return this._ETL_JobCategory;
			}
			set
			{
				if ((this._ETL_JobCategory != value))
				{
					this._ETL_JobCategory = value;
				}
			}
		}
		
		[Column(Storage="_ETL_DateEffective", DbType="DateTime")]
		public System.Nullable<System.DateTime> ETL_DateEffective
		{
			get
			{
				return this._ETL_DateEffective;
			}
			set
			{
				if ((this._ETL_DateEffective != value))
				{
					this._ETL_DateEffective = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
