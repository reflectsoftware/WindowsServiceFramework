using Microsoft.EntityFrameworkCore;
using Plato.Core.Miscellaneous;
using Plato.Interfaces;
using Plato.Threading.WorkManagement;
using System;
using System.Linq;
using WindowsServiceFramework.Extensions.Interfaces;

namespace WindowsServiceFramework.Extensions.Workers
{
    public abstract class PassiveWorker : BaseWorker
    {
        protected readonly IReferenceRepositoryContainer _referenceRepositoryContainer;
        protected readonly Guid _serviceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="PassiveWorker"/> class.
        /// </summary>
        /// <param name="workPackage">The work package.</param>
        /// <param name="referenceRepositoryContainer">The reference repository container.</param>
        public PassiveWorker(
            IWorkPackage workPackage,
            IReferenceRepositoryContainer referenceRepositoryContainer) : base(workPackage)
        {
            Guard.AgainstNull(() => referenceRepositoryContainer);

            _referenceRepositoryContainer = referenceRepositoryContainer;
            _serviceId = Guid.NewGuid();
        }

        /// <summary>
        /// Called when [dispose].
        /// </summary>
        protected override void OnDispose()
        {
            _referenceRepositoryContainer?.Dispose();

            base.OnDispose();
        }

        /// <summary>
        /// Called when [initialize thread].
        /// </summary>
        protected override void OnInitializeThread()
        {
            base.OnInitializeThread();

            var service = _referenceRepositoryContainer
                .PassiveServiceRepository
                .GetAsync(query =>
                {
                    return query
                        .AsNoTracking()
                        .Where(x => x.ServiceName == WorkPackage.NameInstance);

                }).Result;

            if (service == null)
            {
                _referenceRepositoryContainer
                    .PassiveServiceRepository
                    .InitializeSeedAsync(WorkPackage.NameInstance)
                    .Wait();
            }
        }

        /// <summary>
        /// Determines whether the specified time window has ownership.
        /// </summary>
        /// <param name="timeWindow">The time window.</param>
        /// <returns>
        ///   <c>true</c> if the specified time window has ownership; otherwise, <c>false</c>.
        /// </returns>
        protected bool HasOwnership(int timeWindow = 2)
        {
            var result = true;
            if (timeWindow != 0)
            {
                result = _referenceRepositoryContainer
                    .PassiveServiceRepository
                    .ObtainOwnershipAsync(WorkPackage.NameInstance, _serviceId, timeWindow).Result;
            }

            return result;
        }
    }
}
