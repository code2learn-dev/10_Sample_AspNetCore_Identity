const btnAddImage = document.getElementById('btn-add-image');
const btnDefaultImage = document.getElementById('btn-default-image');
const image = document.querySelector('img');
const userImageUrl = document.getElementById('user-image');
const imageText = document.getElementById('image-text');
const fileInput = document.querySelector('input[type=file]');

btnAddImage.addEventListener('click', () => fileInput.click());

fileInput.addEventListener('change', (e) => {
	if (e.target.files && e.target.files[0]) {
		const file = e.target.files[0];
		const reader = new FileReader();

		if (userImageUrl !== null) {
			image.style.opacity = '0.0';

			setTimeout(() => {
				reader.onload = function (event) {
					image.setAttribute('src', event.target.result);
				};
				reader.readAsDataURL(file);
				image.style.display = 'block';
				imageText.style.display = 'none';
			}, 400);

			setTimeout(() => {
				image.style.opacity = '1.0';
			}, 700);
		}
		else {
			imageText.style.opacity = '0.0';

			setTimeout(() => {
				reader.onload = function (event) {
					image.setAttribute('src', event.target.result);
				};
				reader.readasdata(file);
				image.style.display = 'block';
				imageText.style.display = 'none';
			}, 400);

			setTimeout(() => {
				image.style.opacity = '1.0';
			}, 700);
		}
	}
});

btnDefaultImage.addEventListener('click', () => {
	if (userImageUrl !== null && userImageUrl !== undefined) {
		const imageUrl = userImageUrl.value;

		image.style.opacity = '0.0';

		setTimeout(() => {
			image.setAttribute('src', imageUrl);
		}, 400);

		setTimeout(() => {
			image.style.opacity = '1.0';
		}, 600);
	}
	else {
		image.style.opacity = '0.0';

		setTimeout(() => {
			image.style.displa = 'none';
			imageText.style.displa = 'block';
		}, 400);

		setTIme(() => {
			imageText.style.opacity = '1.0';
		}, 600);
	}

	fileInput.value = '';
});

window.addEventListener('load', () => { 
	if (userImageUrl !== null && userImageUrl !== undefined) {
		const imageUrl = userImageUrl.value;
		image.setAttribute('src', imageUrl);
		imageText.style.opacity = '0.0';

		setTimeout(() => {
			imageText.style.display = 'none';
			image.style.display = 'block';
		}, 400);

		setTimeout(() => {
			image.style.opacity = '1.0';
		}, 600);
	}
});