﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region Copyright
// -----------------------------------------------------------------------
// <copyright company="cdmdotnet Limited">
//     Copyright cdmdotnet Limited. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
#endregion
using Cqrs.Domain;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cqrs.Configuration;
using cdmdotnet.Logging;
using Cqrs.Snapshots;
using MyCompany.MyProject.Domain.Authentication.Events;

namespace MyCompany.MyProject.Domain.Authentication
{
	[GeneratedCode("CQRS UML Code Generator", "1.601.786")]
	public  partial class User : AggregateRoot<Cqrs.Authentication.ISingleSignOnToken>
	{
		public Guid Rsn
		{
			get { return Id; }
			private set { Id = value; }
		}

		public bool IsLogicallyDeleted {get; set;}

		#region Implementation of IMessageWithAuthenticationToken<Cqrs.Authentication.ISingleSignOnToken>

		public Cqrs.Authentication.ISingleSignOnToken AuthenticationToken { get; set; }

		#endregion

		protected IDependencyResolver DependencyResolver { get; private set; }

		protected ILogger Log { get; private set; }

// ReSharper disable UnusedMember.Local
		/// <summary>
		/// A constructor for the <see cref="Cqrs.Domain.Factories.IAggregateFactory"/>
		/// </summary>
		private User()
		{

		}

		/// <summary>
		/// A constructor for the <see cref="Cqrs.Domain.Factories.IAggregateFactory"/>
		/// </summary>
		private User(IDependencyResolver dependencyResolver, ILogger log)
		{
			DependencyResolver = dependencyResolver;
			Log = log;
		}
// ReSharper restore UnusedMember.Local

		public User(IDependencyResolver dependencyResolver, ILogger log, Guid rsn)
		{
			DependencyResolver = dependencyResolver;
			Log = log;
			Rsn = rsn;
		}

		/// <summary>
		/// Create a new instance of the <see cref="User"/>
		/// </summary>
		public virtual void CreateUser()
		{
			Log.LogDebug("Entered", "User/CreateUser");
			Log.LogDebug("Pre", "User/OnCreateUser");
			OnCreateUser();
			Log.LogDebug("Post", "User/OnCreateUser");
			Log.LogDebug("Pre", "User/ApplyChange/Create");
			ApplyChange(new UserCreated(Rsn));
			Log.LogDebug("Post", "User/ApplyChange");
			Log.LogDebug("Pre", "User/OnCreatedUser");
			OnCreatedUser();
			Log.LogDebug("Post", "User/OnCreatedUser");
			Log.LogDebug("Exited", "User/CreateUser");
		}

		partial void OnCreateUser();

		partial void OnCreatedUser();

		private void Apply(UserCreated @event)
		{
		}

		/// <summary>
		/// Update an existing instance of the <see cref="User"/>
		/// </summary>
		public virtual void UpdateUser()
		{
			Log.LogDebug("Entered", "User/UpdateUser");
			Log.LogDebug("Pre", "User/OnUpdateUser");
			OnUpdateUser();
			Log.LogDebug("Post", "User/OnUpdateUser");
			Log.LogDebug("Pre", "User/ApplyChange/Update");
			ApplyChange(new UserUpdated(Rsn));
			Log.LogDebug("Post", "User/ApplyChange");
			Log.LogDebug("Pre", "User/OnUpdatedUser");
			OnUpdatedUser();
			Log.LogDebug("Post", "User/OnUpdatedUser");
			Log.LogDebug("Exited", "User/UpdateUser");
		}

		partial void OnUpdateUser();

		partial void OnUpdatedUser();

		private void Apply(UserUpdated @event)
		{
		}

		/// <summary>
		/// Logically delete an existing instance of the <see cref="User"/>
		/// </summary>
		public virtual void DeleteUser()
		{
			Log.LogDebug("Entered", "User/DeleteUser");
			Log.LogDebug("Pre", "User/OnDeleteUser");
			OnDeleteUser();
			Log.LogDebug("Post", "User/OnDeleteUser");
			Log.LogDebug("Pre", "User/ApplyChange/Delete");
			ApplyChange(new UserDeleted(Rsn));
			Log.LogDebug("Post", "User/ApplyChange");
			Log.LogDebug("Pre", "User/OnDeletedUser");
			OnDeletedUser();
			Log.LogDebug("Post", "User/OnDeletedUser");
			Log.LogDebug("Exited", "User/DeleteUser");
		}

		partial void OnDeleteUser();

		partial void OnDeletedUser();

		private void Apply(UserDeleted @event)
		{
			IsLogicallyDeleted = true;
		}

		public class UserSnapshot : Snapshot
		{
			public bool IsLogicallyDeleted {get; set;}
		}
	}
}