using MavenThought.Commons.Testing.Example;
using Moq;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests
{
    /// <summary>
    /// Specification when ...
    /// </summary>
    [Specification]
    public class When_media_library_finds_the_poster : SimpleMovieLibrarySpecification
    {
        /// <summary>
        /// Movie to find the poster for
        /// </summary>
        private IMovie _movie;

        private string _actual;

        /// <summary>
        /// Setup the movie and poster
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movie = MockOf<IMovie>();

            Configure<IPosterService>().Setup(s => s.FindPoster(this._movie)).Returns("MyPoster");
        }

        /// <summary>
        /// Get the poster
        /// </summary>
        protected override void WhenIRun()
        {
            this._actual = this.Sut.Poster(this._movie);
        }

        /// <summary>
        /// Checks that the poster is the expected
        /// </summary>
        [It]
        public void Should_find_the_poster_given_by_the_service()
        {
            this._actual.Should().Be.EqualTo("MyPoster"); 
        }
    }
}