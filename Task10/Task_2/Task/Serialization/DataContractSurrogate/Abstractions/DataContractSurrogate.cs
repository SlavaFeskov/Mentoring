using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Serialization.DataContractSurrogate.Abstractions
{
    public abstract class DataContractSurrogate : IDataContractSurrogate
    {
        public readonly Northwind DbContext;

        protected DataContractSurrogate(Northwind dbContext)
        {
            DbContext = dbContext;
        }

        public abstract Type GetDataContractType(Type type);

        public abstract object GetObjectToSerialize(object obj, Type targetType);

        public virtual object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        public virtual object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public virtual object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public virtual void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public virtual Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public virtual CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration,
            CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }
}