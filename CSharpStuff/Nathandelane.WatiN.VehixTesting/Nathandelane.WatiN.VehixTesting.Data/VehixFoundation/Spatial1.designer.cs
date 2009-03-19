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
	public partial class VehixFoundationSpatialDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public VehixFoundationSpatialDataContext() : 
				base(global::Nathandelane.WatiN.VehixTesting.Data.Properties.Settings.Default.VehixFoundationConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationSpatialDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationSpatialDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationSpatialDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public VehixFoundationSpatialDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<RegionInternalBoundaryTable> RegionInternalBoundaryTables
		{
			get
			{
				return this.GetTable<RegionInternalBoundaryTable>();
			}
		}
		
		public System.Data.Linq.Table<SpatialPointTable> SpatialPointTables
		{
			get
			{
				return this.GetTable<SpatialPointTable>();
			}
		}
		
		public System.Data.Linq.Table<RegionTable> RegionTables
		{
			get
			{
				return this.GetTable<RegionTable>();
			}
		}
		
		public System.Data.Linq.Table<SpatialChainSpatialPointTable> SpatialChainSpatialPointTables
		{
			get
			{
				return this.GetTable<SpatialChainSpatialPointTable>();
			}
		}
		
		public System.Data.Linq.Table<SpatialChainTable> SpatialChainTables
		{
			get
			{
				return this.GetTable<SpatialChainTable>();
			}
		}
	}
	
	[Table(Name="Spatial.RegionInternalBoundaryTable")]
	public partial class RegionInternalBoundaryTable
	{
		
		private long _ID;
		
		private System.Nullable<long> _VFX_CreatorID;
		
		private System.Nullable<long> _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private long _Region;
		
		private long _InternalBoundary;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		public RegionInternalBoundaryTable()
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
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt")]
		public System.Nullable<long> VFX_CreatorID
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
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt")]
		public System.Nullable<long> VFX_UpdaterID
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
		
		[Column(Storage="_Region", DbType="BigInt NOT NULL")]
		public long Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				if ((this._Region != value))
				{
					this._Region = value;
				}
			}
		}
		
		[Column(Storage="_InternalBoundary", DbType="BigInt NOT NULL")]
		public long InternalBoundary
		{
			get
			{
				return this._InternalBoundary;
			}
			set
			{
				if ((this._InternalBoundary != value))
				{
					this._InternalBoundary = value;
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
	
	[Table(Name="Spatial.SpatialPointTable")]
	public partial class SpatialPointTable
	{
		
		private long _ID;
		
		private System.Nullable<long> _VFX_CreatorID;
		
		private System.Nullable<long> _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private double _Latitude;
		
		private double _Longitude;
		
		private System.Nullable<double> _AltitudeInMiles;
		
		private string _ReferenceFrame;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		public SpatialPointTable()
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
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt")]
		public System.Nullable<long> VFX_CreatorID
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
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt")]
		public System.Nullable<long> VFX_UpdaterID
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
		
		[Column(Storage="_Latitude", DbType="Float NOT NULL")]
		public double Latitude
		{
			get
			{
				return this._Latitude;
			}
			set
			{
				if ((this._Latitude != value))
				{
					this._Latitude = value;
				}
			}
		}
		
		[Column(Storage="_Longitude", DbType="Float NOT NULL")]
		public double Longitude
		{
			get
			{
				return this._Longitude;
			}
			set
			{
				if ((this._Longitude != value))
				{
					this._Longitude = value;
				}
			}
		}
		
		[Column(Storage="_AltitudeInMiles", DbType="Float")]
		public System.Nullable<double> AltitudeInMiles
		{
			get
			{
				return this._AltitudeInMiles;
			}
			set
			{
				if ((this._AltitudeInMiles != value))
				{
					this._AltitudeInMiles = value;
				}
			}
		}
		
		[Column(Storage="_ReferenceFrame", DbType="NVarChar(64)")]
		public string ReferenceFrame
		{
			get
			{
				return this._ReferenceFrame;
			}
			set
			{
				if ((this._ReferenceFrame != value))
				{
					this._ReferenceFrame = value;
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
	
	[Table(Name="Spatial.RegionTable")]
	public partial class RegionTable
	{
		
		private long _ID;
		
		private System.Nullable<long> _VFX_CreatorID;
		
		private System.Nullable<long> _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private string _Name;
		
		private long _Boundary;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		private double _MinLatitude;
		
		private double _MaxLatitude;
		
		private double _MinLongitude;
		
		private double _MaxLongitude;
		
		public RegionTable()
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
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt")]
		public System.Nullable<long> VFX_CreatorID
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
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt")]
		public System.Nullable<long> VFX_UpdaterID
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
		
		[Column(Storage="_Name", DbType="NVarChar(32)")]
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
					this._Name = value;
				}
			}
		}
		
		[Column(Storage="_Boundary", DbType="BigInt NOT NULL")]
		public long Boundary
		{
			get
			{
				return this._Boundary;
			}
			set
			{
				if ((this._Boundary != value))
				{
					this._Boundary = value;
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
		
		[Column(Storage="_MinLatitude", DbType="Float NOT NULL")]
		public double MinLatitude
		{
			get
			{
				return this._MinLatitude;
			}
			set
			{
				if ((this._MinLatitude != value))
				{
					this._MinLatitude = value;
				}
			}
		}
		
		[Column(Storage="_MaxLatitude", DbType="Float NOT NULL")]
		public double MaxLatitude
		{
			get
			{
				return this._MaxLatitude;
			}
			set
			{
				if ((this._MaxLatitude != value))
				{
					this._MaxLatitude = value;
				}
			}
		}
		
		[Column(Storage="_MinLongitude", DbType="Float NOT NULL")]
		public double MinLongitude
		{
			get
			{
				return this._MinLongitude;
			}
			set
			{
				if ((this._MinLongitude != value))
				{
					this._MinLongitude = value;
				}
			}
		}
		
		[Column(Storage="_MaxLongitude", DbType="Float NOT NULL")]
		public double MaxLongitude
		{
			get
			{
				return this._MaxLongitude;
			}
			set
			{
				if ((this._MaxLongitude != value))
				{
					this._MaxLongitude = value;
				}
			}
		}
	}
	
	[Table(Name="Spatial.SpatialChainSpatialPointTable")]
	public partial class SpatialChainSpatialPointTable
	{
		
		private long _ID;
		
		private System.Nullable<long> _VFX_CreatorID;
		
		private System.Nullable<long> _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private long _SpatialChain;
		
		private long _SpatialPoint;
		
		private int _Sequence;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		public SpatialChainSpatialPointTable()
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
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt")]
		public System.Nullable<long> VFX_CreatorID
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
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt")]
		public System.Nullable<long> VFX_UpdaterID
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
		
		[Column(Storage="_SpatialChain", DbType="BigInt NOT NULL")]
		public long SpatialChain
		{
			get
			{
				return this._SpatialChain;
			}
			set
			{
				if ((this._SpatialChain != value))
				{
					this._SpatialChain = value;
				}
			}
		}
		
		[Column(Storage="_SpatialPoint", DbType="BigInt NOT NULL")]
		public long SpatialPoint
		{
			get
			{
				return this._SpatialPoint;
			}
			set
			{
				if ((this._SpatialPoint != value))
				{
					this._SpatialPoint = value;
				}
			}
		}
		
		[Column(Storage="_Sequence", DbType="Int NOT NULL")]
		public int Sequence
		{
			get
			{
				return this._Sequence;
			}
			set
			{
				if ((this._Sequence != value))
				{
					this._Sequence = value;
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
	
	[Table(Name="Spatial.SpatialChainTable")]
	public partial class SpatialChainTable
	{
		
		private long _ID;
		
		private System.Nullable<long> _VFX_CreatorID;
		
		private System.Nullable<long> _VFX_UpdaterID;
		
		private System.Nullable<long> _VFX_DeletorID;
		
		private System.DateTime _VFX_DateCreated;
		
		private System.DateTime _VFX_DateUpdated;
		
		private System.Nullable<System.DateTime> _VFX_DateDeleted;
		
		private string _Name;
		
		private string _ETL_NaturalKey;
		
		private System.Data.Linq.Binary _ETL_Hash;
		
		private string _ETL_JobCategory;
		
		private System.Nullable<System.DateTime> _ETL_DateEffective;
		
		public SpatialChainTable()
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
		
		[Column(Storage="_VFX_CreatorID", DbType="BigInt")]
		public System.Nullable<long> VFX_CreatorID
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
		
		[Column(Storage="_VFX_UpdaterID", DbType="BigInt")]
		public System.Nullable<long> VFX_UpdaterID
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
		
		[Column(Storage="_Name", DbType="NVarChar(32)")]
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
					this._Name = value;
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
