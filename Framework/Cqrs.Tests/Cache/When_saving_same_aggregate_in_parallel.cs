﻿using System;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Chinchilla.Logging;
using Cqrs.Cache;
using Cqrs.Domain;
using Cqrs.Domain.Factories;
using Cqrs.Authentication;
using Cqrs.Configuration;
using Cqrs.Tests.Substitutes;
using NUnit.Framework;

namespace Cqrs.Tests.Cache
{
	/// <summary>
	/// All these tests just test, test code.
	/// </summary>
	public class When_saving_same_aggregate_in_parallel
	{
		private CacheRepository<ISingleSignOnToken> _rep1;
		private CacheRepository<ISingleSignOnToken> _rep2;
		private TestAggregate _aggregate;
		private TestInMemoryEventStore _testStore;

		[SetUp]
		public void Setup()
		{
			// This will clear the cache between runs.
			var cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
			foreach (var cacheKey in cacheKeys)
				MemoryCache.Default.Remove(cacheKey);

			var dependencyResolver = new TestDependencyResolver(null);
			var aggregateFactory = new AggregateFactory(dependencyResolver, dependencyResolver.Resolve<ILogger>());
			_testStore = new TestInMemoryEventStore();
			_rep1 = new CacheRepository<ISingleSignOnToken>(new AggregateRepository<ISingleSignOnToken>(aggregateFactory, _testStore, new TestEventPublisher(), new NullCorrelationIdHelper(), new ConfigurationManager()), _testStore);
			_rep2 = new CacheRepository<ISingleSignOnToken>(new AggregateRepository<ISingleSignOnToken>(aggregateFactory, _testStore, new TestEventPublisher(), new NullCorrelationIdHelper(), new ConfigurationManager()), _testStore);

			_aggregate = new TestAggregate(Guid.NewGuid());
			_rep1.Save(_aggregate);

			var t1 = new Task(() =>
								  {
									  for (var i = 0; i < 100; i++)
									  {
										  var aggregate = _rep1.Get<TestAggregate>(_aggregate.Id);
										  aggregate.DoSomething();
										  _rep1.Save(aggregate);
									  }
								  });

			var t2 = new Task(() =>
								  {
									  for (var i = 0; i < 100; i++)
									  {
										  var aggregate = _rep2.Get<TestAggregate>(_aggregate.Id);
										  aggregate.DoSomething();
										  _rep2.Save(aggregate);
									  }
								  });
			var t3 = new Task(() =>
								  {
									  for (var i = 0; i < 100; i++)
									  {
										  var aggregate = _rep2.Get<TestAggregate>(_aggregate.Id);
										  aggregate.DoSomething();
										  _rep2.Save(aggregate);
									  }
								  });
			t1.Start();
			t2.Start();
			t3.Start();

			Task.WaitAll(new[] {t1,t2, t3});
		}

		//[Test]
		public void Should_not_get_more_than_one_event_with_same_id()
		{
			Assert.That(_testStore.Events.Select(x => x.Version).Distinct().Count(), Is.EqualTo(_testStore.Events.Count));
		}

		//[Test]
		public void Should_save_all_events()
		{
			Assert.That(_testStore.Events.Count(), Is.EqualTo(295));
		}
	}
}