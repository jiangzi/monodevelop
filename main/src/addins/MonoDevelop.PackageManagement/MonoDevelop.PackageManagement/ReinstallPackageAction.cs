//
// ReinstallPackageAction.cs
//
// Author:
//       Matt Ward <matt.ward@xamarin.com>
//
// Copyright (c) 2014 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide.Commands;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.PackageManagement.NodeBuilders;
using MonoDevelop.Projects;
using ICSharpCode.PackageManagement;
using NuGet;

namespace MonoDevelop.PackageManagement
{
	public class ReinstallPackageAction : ProcessPackageAction
	{
		public ReinstallPackageAction (
			IPackageManagementProject project,
			IPackageManagementEvents packageManagementEvents)
			: base (project, packageManagementEvents)
		{
		}

		protected override string StartingMessageFormat {
			get { return "Reinstalling {0}..." + Environment.NewLine; }
		}

		protected override void ExecuteCore ()
		{
			UninstallPackage ();
			InstallPackage ();
		}

		void UninstallPackage ()
		{
			Logger.Log (MessageLevel.Info, GettextCatalog.GetString ("Uninstalling {0}...", Package.Id));

			UninstallPackageAction action = Project.CreateUninstallPackageAction ();
			action.Package = Package;
			action.Execute ();
		}

		void InstallPackage ()
		{
			Logger.Log (MessageLevel.Info, GettextCatalog.GetString ("Installing {0}...", Package.Id));

			InstallPackageAction action = Project.CreateInstallPackageAction ();
			action.Package = Package;
			action.Execute ();
		}
	}
}
