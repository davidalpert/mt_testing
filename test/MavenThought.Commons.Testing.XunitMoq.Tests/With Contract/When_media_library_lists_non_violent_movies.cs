using System.Collections.Generic;
using System.Linq;
using MavenThought.Commons.Testing.Example;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests
{
    /// <summary>
    /// Specification when listing non violent movies
    /// </summary>
    [Specification]
    public class When_media_library_lists_non_violent_movies : SimpleMovieLibrarySpecification
    {
        private ICollection<IMovie> _movies;

        private IEnumerable<IMovie> _actual;

        /// <summary>
        /// Setup the critic
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movies = new List<IMovie>();

            for (var i = 0; i < 10; i++)
            {
                this._movies.Add(MockOf<IMovie>());
            }

            foreach (var movie in _movies.Take(5))
            {
                var movie1 = movie;
                
                Configure<IMovieCritic>().Setup(c => c.IsViolent(movie1)).Returns(true);
            }
        }

        /// <summary>
        /// Setup the movies
        /// </summary>
        protected override void AndGivenThatAfterCreated()
        {
            base.AndGivenThatAfterCreated();

            foreach (var movie in _movies)
            {
                this.Sut.Add(movie);
            }
        }

        /// <summary>
        /// List non violent movies
        /// </summary>
        protected override void WhenIRun()
        {
            this._actual = this.Sut.ListNonViolent();
        }

        /// <summary>
        /// Checks that the movies returned are non violent
        /// </summary>
        [It]
        public void Should_return_only_non_violent_movies()
        {
            this._actual.Should().Have.SameSequenceAs(this._movies.Skip(5));
        }
    }
}