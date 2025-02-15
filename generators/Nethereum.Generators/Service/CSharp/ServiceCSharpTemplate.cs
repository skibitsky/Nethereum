using Nethereum.Generators.Core;
using Nethereum.Generators.CQS;

namespace Nethereum.Generators.Service
{
    public class ServiceCSharpTemplate: ClassTemplateBase<ServiceModel>
    {
        private FunctionServiceMethodCSharpTemplate _functionServiceMethodCSharpTemplate;
        private ContractDeploymentServiceMethodsCSharpTemplate _deploymentServiceMethodsCSharpTemplate;
        public ServiceCSharpTemplate(ServiceModel model):base(model)
        {
            _functionServiceMethodCSharpTemplate = new FunctionServiceMethodCSharpTemplate(model);
            _deploymentServiceMethodsCSharpTemplate = new ContractDeploymentServiceMethodsCSharpTemplate(model);
            ClassFileTemplate = new CSharpClassFileTemplate(Model, this);
        }

        public override string GenerateClass()
        {
            return
                $@"{GenerateMainClass()}
{SpaceUtils.NoTabs}
{SpaceUtils.NoTabs}
{GenerateBaseClass()}";
        }


        public string GenerateMainClass()
        {
            return
                $@"{SpaceUtils.One__Tab}public partial class {Model.GetTypeName()}: {Model.GetTypeName()}Base
{SpaceUtils.One__Tab}{{
{_deploymentServiceMethodsCSharpTemplate.GenerateMethods()}
{SpaceUtils.NoTabs}
{SpaceUtils.Two___Tabs}public {Model.GetTypeName()}(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
{SpaceUtils.Two___Tabs}{{
{SpaceUtils.Two___Tabs}}}

{SpaceUtils.One__Tab}}}";
        }


        public string GenerateBaseClass()
        {
            return
                $@"{SpaceUtils.One__Tab}public partial class {Model.GetTypeName()}Base: ContractWeb3ServiceBase
{SpaceUtils.One__Tab}{{
{SpaceUtils.NoTabs}
{SpaceUtils.Two___Tabs}public {Model.GetTypeName()}Base(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
{SpaceUtils.Two___Tabs}{{
{SpaceUtils.Two___Tabs}}}
{SpaceUtils.NoTabs}
{_functionServiceMethodCSharpTemplate.GenerateMethods()}
{SpaceUtils.One__Tab}}}";
        }
    }
}