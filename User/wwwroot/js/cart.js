function addToCart(productId) {
	fetch('/Cart/AddToCart', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/x-www-form-urlencoded'
		},
		body: `productId=${productId}&quantity=1`
	})
		.then(res => res.json())
		.then(data => {
			if (data.success) {
				showToast(data.message, true);
				updateCartBadge();
			} else {
				showToast(data.message, false);
			}
		});
}

