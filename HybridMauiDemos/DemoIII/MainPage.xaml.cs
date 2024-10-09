using System.Diagnostics;

namespace DemoIII;

public partial class MainPage : ContentPage
{

	// IBleService _bleService = new BleService();
	IBleService _bleService = new FakeBleService();
	public MainPage()
	{
		InitializeComponent();

  		_bleService.HeartRateValue += (sender, heartRate) =>
		{
			MainThread.BeginInvokeOnMainThread(() => { hybridWebView.SendRawMessage($"{heartRate}"); });
			// MainThread.BeginInvokeOnMainThread( async () => { await hybridWebView.InvokeJavaScriptAsync("updateHeartRate", [heartRate], [HeartRateJsContext.Default.Int32]); });
			Debug.WriteLine(heartRate);
		};
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		ActivityIndicator.IsRunning = true;
		ActivityIndicator.IsVisible = true;
		await _bleService.ConnectToPolar();
		ActivityIndicator.IsRunning = false;
		ActivityIndicator.IsVisible = false;
	}
}
