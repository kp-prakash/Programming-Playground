#region References
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection; 
#endregion

namespace Utils
{
    public class MefComposer
    {
        #region Private Constants
        private const string DefaultTargetPath = "../Target/"; 
        #endregion
        
        #region Private Member
        private Collection<AssemblyCatalog> _assemblyCatalogCollection;
        private AggregateCatalog _aggregateCatalog;
        private Collection<DirectoryCatalog> _directoryCatalogCollection;
        private CompositionContainer _compositionContainer; 
        #endregion

        #region Private Constructor
        private MefComposer()
        {
            SetDefaultDirectoryCatalog();
        }
        #endregion

        #region Public Properties
        public AggregateCatalog AggregateCatalog
        {
            get { return _aggregateCatalog ?? (_aggregateCatalog = new AggregateCatalog()); }
        }

        public CompositionContainer CompositionContainer
        {
            get { return _compositionContainer ?? (_compositionContainer = new CompositionContainer(AggregateCatalog)); }
        }

        public Collection<DirectoryCatalog> DirectoryCatalogCollection
        {
            get { return _directoryCatalogCollection ?? (_directoryCatalogCollection = new Collection<DirectoryCatalog>()); }
        }

        public Collection<AssemblyCatalog> AssemblyCatalogCollection
        {
            get { return _assemblyCatalogCollection ?? (_assemblyCatalogCollection = new Collection<AssemblyCatalog>()); }
        } 
        #endregion

        #region Static Properties
        public static MefComposer Instance
        {
            get { return Singleton<MefComposer>.Instance; }
        } 
        #endregion

        #region Public Methods
        public void SetDefaultDirectoryCatalog()
        {
            AddDirectoryCatalog(DefaultTargetPath);
        }

        public void AddDirectoryCatalog(string directoryCatalogPath)
        {
            var directoryCatalogQuery = from DirectoryCatalog directoryCatalogInCache in DirectoryCatalogCollection
                                        where directoryCatalogInCache.Path == directoryCatalogPath
                                        select directoryCatalogInCache;
            if (!directoryCatalogQuery.Any())
            {
                var directoryCatalog = new DirectoryCatalog(directoryCatalogPath);
                _directoryCatalogCollection.Add(directoryCatalog);
                AggregateCatalog.Catalogs.Add(directoryCatalog);
            }
        }

        public AssemblyCatalog GetAssemblyCatalogFromCollection()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyCatalogs = (from AssemblyCatalog assemblyCatalogInCache in AssemblyCatalogCollection
                                    where assemblyCatalogInCache.Assembly.FullName == assembly.FullName
                                    select assemblyCatalogInCache).ToList();
            if (assemblyCatalogs.Count() == 1)
                return assemblyCatalogs.First();
            var assemblyCatalog = new AssemblyCatalog(assembly);
            AssemblyCatalogCollection.Add(assemblyCatalog);
            return assemblyCatalog;
        }

        public void ComposeParts(object obj)
        {
            AssemblyCatalog currentAssemblyCatalog = GetAssemblyCatalogFromCollection();
            if (!AggregateCatalog.Catalogs.Contains(currentAssemblyCatalog))
                AggregateCatalog.Catalogs.Add(currentAssemblyCatalog);
            CompositionContainer.ComposeParts(obj);
        } 
        #endregion
    }
}