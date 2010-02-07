using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace NHibernateProfiler.UT
{
    /// <summary>
    /// Chain UT
    /// bstack @ 22/01/2010
    /// </summary>
    public class Chain
    {
		private readonly MockRepository c_mockRepository;


		/// <summary>
		/// Ctor
		/// </summary>
		public Chain()
		{
			this.c_mockRepository = new MockRepository();
		}


        [Fact]
		public void Ctor_Valid_Demonstrate_One_Parser_Call_One_Parser_No_Call()
        {
			// Arrange
			var _chain = this.BuildChain();
			using (this.c_mockRepository.Record())
			{
				Expect.Call(_chain.ParserCache.First.Value.MustParse(null, null)).IgnoreArguments().Return(true);
				Expect.Call(_chain.ParserCache.First.Value.Parse(null, null)).IgnoreArguments().Return(null);
				Expect.Call(_chain.ParserCache.First.Next.Value.MustParse(null, null)).IgnoreArguments().Return(false);
			}

			// Act
			using (this.c_mockRepository.Playback())
			{
				_chain.ResolveParameters(new string[] { "asdasdads", "sddfsfsf", "dffggdgd" });
			}
			
			// Assert
            Assert.NotNull(_chain);
            Assert.NotNull(_chain.ParserCache);
            Assert.True(_chain.ParserCache.Count == 2);
        }


		[Fact]
		public void Ctor_Valid_Demonstrate_Two_Parser_Call()
		{
			// Arrange
			var _chain = this.BuildChain();
			using (this.c_mockRepository.Record())
			{
				Expect.Call(_chain.ParserCache.First.Value.MustParse(null, null)).IgnoreArguments().Return(true);
				Expect.Call(_chain.ParserCache.First.Value.Parse(null, null)).IgnoreArguments().Return(null);
				Expect.Call(_chain.ParserCache.First.Next.Value.MustParse(null, null)).IgnoreArguments().Return(true);
				Expect.Call(_chain.ParserCache.First.Next.Value.Parse(null, null)).IgnoreArguments().Return(null);
			}

			// Act
			using (this.c_mockRepository.Playback())
			{
				_chain.ResolveParameters(new string[] { "asdasdads", "sddfsfsf", "dffggdgd" });
			}

			// Assert
			Assert.NotNull(_chain);
			Assert.NotNull(_chain.ParserCache);
			Assert.True(_chain.ParserCache.Count == 2);
		}


		[Fact]
		public void Ctor_Valid_Demonstrate_No_Call()
		{
			// Arrange
			var _chain = this.BuildChain();
			using (this.c_mockRepository.Record())
			{
				Expect.Call(_chain.ParserCache.First.Value.MustParse(null, null)).IgnoreArguments().Return(false);
				Expect.Call(_chain.ParserCache.First.Next.Value.MustParse(null, null)).IgnoreArguments().Return(false);
			}

			// Act
			using (this.c_mockRepository.Playback())
			{
				_chain.ResolveParameters(new string[] { "asdasdads", "sddfsfsf", "dffggdgd" });
			}

			// Assert
			Assert.NotNull(_chain);
			Assert.NotNull(_chain.ParserCache);
			Assert.True(_chain.ParserCache.Count == 2);
		}


		/// <summary>
		/// Build chain
		/// </summary>
		/// <returns>Mocked up chain</returns>
		public NHibernateProfiler.PreparedStatementParameter.Chain BuildChain()
		{
			var _parserCache = new LinkedList<NHibernateProfiler.PreparedStatementParameter.Parser.IParser>();

			_parserCache.AddFirst(this.c_mockRepository.StrictMock<NHibernateProfiler.PreparedStatementParameter.Parser.IParser>());
			_parserCache.AddLast(this.c_mockRepository.StrictMock<NHibernateProfiler.PreparedStatementParameter.Parser.IParser>());
			
			return new NHibernateProfiler.PreparedStatementParameter.Chain(_parserCache);
		}
    }
}
