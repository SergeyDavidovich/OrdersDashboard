using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// https://bipinpaul.com/posts/display-spinner-on-each-api-call-automatically-in-blazor

namespace OrdersDashboard.Client.Spinner
{
	public class SpinnerService
	{
		public event Action OnShow;
		public event Action OnHide;

		public void Show()
		{
			OnShow?.Invoke();
		}

		public void Hide()
		{
			OnHide?.Invoke();
		}
	}
}
