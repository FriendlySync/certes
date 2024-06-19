using System.Collections.Generic;
using System.Threading.Tasks;
using Certes.Acme.Resource;

namespace Certes.Acme
{
    /// <summary>
    /// Presents the context for ACME order operations.
    /// </summary>
    public interface IOrderContext : IResourceContext<Order>
    {
        /// <summary>
        /// Gets the authorizations for this order.
        /// </summary>
        /// <returns>
        /// The list of authorizations.
        /// </returns>
        Task<IEnumerable<IAuthorizationContext>> Authorizations();

        /// <summary>
        /// Finalizes the certificate order.
        /// </summary>
        /// <param name="csr">The CSR in DER.</param>
        /// <returns>The order finalized.</returns>
        Task<Order> Finalize(byte[] csr);
        
       /// <summary>
       /// 
       /// </summary>
       /// <param name="preferredChain"></param>
       /// <returns></returns>
        Task<CertificateChain> Download(string preferredChain = null);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="certPath"></param>
        /// <returns></returns>
        Task DownloadCertificateToFile(string certPath);
    }
}
