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
using Cqrs.Repositories.Queries;

namespace MyCompany.MyProject.Domain.Inventory.Repositories.Queries.Strategies
{
	[GeneratedCode("CQRS UML Code Generator", "1.500.508.396")]
	public partial interface IInventoryItemQueryStrategy : IQueryStrategy
	{
		InventoryItemQueryStrategy WithRsn(Guid rsn);

	}
}