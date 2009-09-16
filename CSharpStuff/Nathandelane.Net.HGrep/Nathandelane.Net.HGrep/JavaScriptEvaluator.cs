﻿/*  Copyright (C) 2009, Nathandelane.
	License:
	Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

	This program is free software; you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation; either version 3, or (at your option)
	any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
	02111-1307, USA.
*/

using HtmlAgilityPack;
using EcmaScript.NET;

namespace Nathandelane.Net.HGrep
{
	public class JavaScriptEvaluator
	{
		#region Fields

		private HtmlDocument _document;

		#endregion

		#region Constructors

		public JavaScriptEvaluator(string document)
		{
			_document = new HtmlDocument();
			_document.LoadHtml(document);
		}

		public JavaScriptEvaluator(HtmlDocument document)
		{
			_document = document;
		}

		#endregion

		#region Methods

		public void Evaluate()
		{
			
		}

		#endregion
	}
}
