using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DataFactory.Common;
using DataFactory.Data.Model;
using DataFactory.Data.Model.Base;

namespace DataFactory.Data.Helpers
{
    public static class Checksum
    {

        public static string CalculateMd5Hash(StringBuilder checksum)
        {
            var md5Hash = Utils.CalculateMD5Hash(checksum.ToString());

            return md5Hash;
        }

        public static StringBuilder CalculateContainersChecksum(List<Container> containers)
        {
            // Checksum is calculated based on container id, all asset ids for the container and the total asset count

            var checksum = new StringBuilder();

            foreach (var container in containers)
            {
                checksum.Append(container.Id);

                //var debugValue1 = context.Containers.FirstOrDefault(c => c.Id == container.Id);
                //var debugValue2 = debugValue1.Assets.OrderByDescending(a => a.Id).ToList();

                //var assets =
                //context.Containers.FirstOrDefault(c => c.Id == container.Id)
                //    .Assets.OrderByDescending(a => a.Id)
                //    .ToList();

                foreach (var asset in container.Assets.OrderByDescending(a => a.Id))
                {
                    checksum.Append(asset.Id);
                }

                checksum.Append(container.Assets.Count);
            }
            
            return checksum;
        }



        //public static StringBuilder CalculateClientChecksum(List<BaseClient> clients)
        //{
        //    var checksum = new StringBuilder();

        //    foreach (var client in clients)
        //    {
        //        checksum.Append(client.Id);

        //        foreach (var asset in client.Projects.OrderByDescending(a => a.Id))
        //        {
        //            checksum.Append(asset.Id);
        //        }

        //        checksum.Append(client.Projects.Count);
        //    }

        //    return checksum;
        //}

        public static StringBuilder CalculateHierarchyChecksum(List<BaseHierarchyStructure> structures)
        {
            var checksum = new StringBuilder();

            foreach (var structure in structures)
            {
                checksum.Append(structure.Id);
            }

            return checksum;
        }

        //public static StringBuilder CalculateContactChecksum(List<Contact> contacts)
        //{
        //    var checksum = new StringBuilder();

        //    foreach (var contact in contacts)
        //    {
        //        checksum.Append(contact.Id);
        //    }

        //    return checksum;
        //}

        //public static StringBuilder CalculateRoomTypeChecksum(List<RoomType> roomTypes)
        //{
        //    var checksum = new StringBuilder();

        //    foreach (var rt in roomTypes)
        //    {
        //        checksum.Append(rt.Id);
        //    }

        //    return checksum;
        //}

        //public static StringBuilder CalculateProjectChecksum(List<Project> projects)
        //{
        //      var checksum = new StringBuilder();

        //    foreach (var project in projects)
        //    {
        //        checksum.Append(project.Id);
        //    }

        //    return checksum;
        //}

        //public static StringBuilder CalculateProjectVerifiersChecksum(List<ProjectVerifier> list)
        //{
        //    var checksum = new StringBuilder();

        //    foreach (var pv in list)
        //    {
        //        checksum.Append(pv.Id);
        //    }

        //    return checksum;
        //}

        public static StringBuilder CalculateModelChecksum<T>(List<T> list)
        {
            var checksum = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (T pv in list)
            {
                foreach (var property in properties.Where(property => property.Name == "Id"))
                {
                    checksum.Append(property.GetValue(pv, null).ToString());
                }
            }

            return checksum;
        }

        public static string GetModelMD5Hash<T>(List<T> list)
        {
            return CalculateMd5Hash(CalculateModelChecksum(list));
        }
    }
}
