using System.Net;
using System.Net.Sockets;
using Microsoft.Maui.Controls;

namespace BTAI
{
    public partial class MainPage : ContentPage
    {
		private string aiServiceUrl = "https://chat.cqbaitao.cn:30443";

		public MainPage()
        {
            InitializeComponent();
			CheckIPv6AndLoad();
            AiWebView.Navigated += AiWebView_Navigated;
		}

        private async void CheckIPv6AndLoad()
		{
			bool hasIPv6 = await HasIPv6Connectivity();

			if (hasIPv6)
			{
				AiWebView.Source = aiServiceUrl;
			}
			else
			{
				await DisplayAlert("网络提示", "当前网络不支持 IPv6，无法连接 AI 服务。", "确定");
			}
		}

        private async Task<bool> HasIPv6Connectivity()
        {
            try
            {
                // 获取域名的所有 IP 地址，包括 IPv6 和 IPv4
                var ipAddresses = await Dns.GetHostAddressesAsync("chat.cqbaitao.cn");

                // 查找 IPv6 地址
                var ipv6Address = ipAddresses.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);

                if (ipv6Address == null)
                {
                    return false; // 没有 IPv6 地址
                }

                // 尝试通过 Socket 连接到 IPv6 地址的 端口 3000（如果是 HTTP 服务）
                using (var socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp))
                {
                    // 尝试连接到 IPv6 地址和端口
                    await TryConnectAsync(ipv6Address, 3000);
                    return true; // 如果连接成功，返回 true
                }
            }
            catch
            {
                // 连接失败或者无法找到 IPv6 地址
                return false; // 返回 false，表示不支持 IPv6
            }
        }

        // 当 WebView 页面加载完毕后执行注入
        private async void AiWebView_Navigated(object sender, WebNavigatedEventArgs e)
		{
			// 注入 JavaScript 脚本，确保对话区域启用滚动条
			await AiWebView.EvaluateJavaScriptAsync("document.querySelector('.chat-content').style.overflowY = 'auto';");
			await AiWebView.EvaluateJavaScriptAsync("document.querySelector('.chat-content').style.maxHeight = '100vh';");
		}

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            AiWebView.Reload();
        }

        private async Task<bool> TryConnectAsync(IPAddress address, int port, int timeoutMilliseconds = 5000)
        {
            try
            {
                using (var socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    var connectTask = socket.ConnectAsync(new IPEndPoint(address, port));

                    if (await Task.WhenAny(connectTask, Task.Delay(timeoutMilliseconds)) == connectTask)
                    {
                        // 成功或失败，connectTask已经完成
                        return socket.Connected;
                    }
                    else
                    {
                        // 超时了，关闭 socket
                        socket.Close();
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }

}
