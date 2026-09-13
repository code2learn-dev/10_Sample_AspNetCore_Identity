const btnAddImage = document.getElementById('btn-add-image');
const btnRemoveImage = document.getElementById('btn-remove-image');
const btnDefaultImage = document.getElementById('btn-default-image');
const image = document.querySelector('img');
const fileInput = document.querySelector('input[type=file]');
const imageText = document.getElementById('image-text');

btnAddImage.addEventListener('click', () => fileInput.click());
btnRemoveImage.addEventListener('click', () => {
	image.style.opacity = '0.0';
	setTimeout(() => {
		image.style.display = 'none';
		imageText.style.display = 'block';
	}, 400);
	setTimeout(() => {
		imageText.style.opacity = '1.0';
	}, 600);

	fileInput.value = '';
}); 

fileInput.addEventListener('change', (e) => {
	if (e.target.files && e.target.files[0]) {
		const file = e.target.files[0];
		const reader = new FileReader();

		imageText.style.opacity = '0.0';

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
});

