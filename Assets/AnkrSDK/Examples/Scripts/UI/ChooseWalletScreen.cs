using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AnkrSDK.UI
{
	public class ChooseWalletScreen : MonoBehaviour
	{
		[SerializeField] private Transform _buttonGridTransform;
		[SerializeField] private GameObject _loadingText;
		[SerializeField] private WalletButton _buttonPrefab;

		private Action<AnkrSDK.WalletConnectSharp.Unity.Models.DeepLink.AppEntry> ClickAction { get; set; }

		private bool _isActivated;

		public void Activate(Action<AnkrSDK.WalletConnectSharp.Unity.Models.DeepLink.AppEntry> action)
		{
			ClickAction = action;
			gameObject.SetActive(true);
			_isActivated = true;
			BuildWalletButtons().Forget();
		}

		private async UniTask BuildWalletButtons()
		{
			_loadingText.SetActive(true);
			var fetchWalletList = await WalletConnectSharp.Unity.Utils.WalletDownloadHelper.FetchWalletList(true);

			foreach (var kvp in fetchWalletList)
			{
				var walletData = kvp.Value;
				var walletObj = Instantiate(_buttonPrefab, _buttonGridTransform);

				walletObj.Setup(walletData);
				walletObj.SetListener(() => ClickAction?.Invoke(walletData));
			}

			_loadingText.SetActive(false);
		}

		public void SetActive(bool isConnected)
		{
			gameObject.SetActive(isConnected && _isActivated);
		}
	}
}