using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.HostsFileSetter
{
	/// <summary>
	/// All Hosts files configurations should implement this interface.
	/// </summary>
	public abstract class AbstractHostsFileConfiguration
	{
		#region Fields

		private string _name;
		private string _comments;
		private IList<DnsEntry> _entries;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the name of this configuration.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Gets any comments that will appear at the top of the hosts file configuration.
		/// </summary>
		public string Comments
		{
			get { return _comments; }
		}

		/// <summary>
		/// Gets a list of Dns Entries that will appear in the hosts file configuration.
		/// </summary>
		public IList<DnsEntry> Entries
		{
			get { return _entries; }
		}

		#endregion

		#region Constructors

		public AbstractHostsFileConfiguration(string name)
		{
			_name = name;
			_comments = String.Empty;
			_entries = new List<DnsEntry>();
		}

		public AbstractHostsFileConfiguration(string name, string comments)
		{
			_name = name;
			_comments = comments;
			_entries = new List<DnsEntry>();
		}

		public AbstractHostsFileConfiguration(string name, IEnumerable<DnsEntry> entries)
		{
			_name = name;
			_comments = String.Empty;
			_entries = new List<DnsEntry>(entries);
		}

		public AbstractHostsFileConfiguration(string name, string comments, IEnumerable<DnsEntry> entries)
		{
			_name = name;
			_comments = comments;
			_entries = new List<DnsEntry>(entries);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a DnsEntry to the entries list.
		/// </summary>
		/// <param name="entry"></param>
		public void AddEntry(DnsEntry entry)
		{
			_entries.Add(entry);
		}

		public override string ToString()
		{
			return _name;
		}

		#endregion
	}
}
