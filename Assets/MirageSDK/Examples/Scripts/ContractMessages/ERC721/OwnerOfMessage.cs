using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace MirageSDK.Examples.Scripts.ContractMessages.ERC721
{
	[Function("ownerOf", "address")]
	public class OwnerOfMessage : FunctionMessage
	{
		[Parameter("uint256", "_tokenId", 1)]
		public string TokenId { get; set; }
		
	}
}