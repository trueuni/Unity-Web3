﻿using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AnkrSDK.WalletConnectSharp.Core.Models
{
	public class GenericJsonRpcResponse : JsonRpcResponse
	{
		[JsonProperty("result")]
		public JToken Result { get; private set; }

		public RpcResponseMessage ToRpcResponseMessage()
		{
			if (IsError)
			{
				var errorJson = JsonConvert.SerializeObject(Error);
				var rpcError = JsonConvert.DeserializeObject<RpcError>(errorJson);
				return new RpcResponseMessage(ID, rpcError);
			}
			else
			{
				return new RpcResponseMessage(ID, Result);
			}
		}
	}
}