using System;
using MavenThought.Commons.Testing.Example;
using Moq;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests
{
    /// <summary>
    /// Specification when adding a movie
    /// </summary>
    [Specification]
    public class When_media_library_adds_a_movie : SimpleMovieLibrarySpecification
    {
        private IMovie _movie;
        private EventHandler<MediaLibraryArgs> _handler;

        /// <summary>
        /// Setup the movie
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movie = MockOf<IMovie>();

            this._handler = MockOf<EventHandler<MediaLibraryArgs>>();
        }

        /// <summary>
        /// Register the handler
        /// </summary>
        protected override void AndGivenThatAfterCreated()
        {
            base.AndGivenThatAfterCreated();

            this.Sut.Added += this._handler;
        }

        /// <summary>
        /// Add the movie
        /// </summary>
        protected override void WhenIRun()
        {
            this.Sut.Add(_movie);
        }

        /// <summary>
        /// Checks that movie has been added
        /// </summary>
        [It]
        public void Should_add_the_movie_to_the_library()
        {
            this.Sut.Contents.Should().Have.SameSequenceAs(this._movie);
        }

        /// <summary>
        /// Checks that notifies the addition
        /// </summary>
        [It]
        public void Should_notify_the_movie_was_added()
        {
             MockOf(this._handler).Verify(h => h(this.Sut,
                                                 It.Is<MediaLibraryArgs>(
                                                    arg => arg.Movie == this._movie)));
        }
    }
}