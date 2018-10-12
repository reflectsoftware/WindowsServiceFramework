using System;

namespace WindowsServiceFramework.Extensions.Interfaces
{
    public interface IWorkerRepositoryContainer : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IWorkerRepositoryContainer"/> is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        bool Disposed { get; }

        ///// <summary>
        ///// Gets the database context.
        ///// </summary>
        ///// <value>
        ///// The database context.
        ///// </value>
        //IReferenceContext DbContext { get; }

        /// <summary>
        /// Gets the passive service repository.
        /// </summary>
        /// <value>
        /// The passive service repository.
        /// </value>
        IPassiveServiceRepository PassiveServiceRepository { get; }

        /// <summary>
        /// Gets the delay service repository.
        /// </summary>
        /// <value>
        /// The delay service repository.
        /// </value>
        IDelayServiceRepository DelayServiceRepository { get; }
    }
}
