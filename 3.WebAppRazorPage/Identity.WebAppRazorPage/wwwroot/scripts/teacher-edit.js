const btnUpdateImage = document.getElementById('btn-update-image');
const btnDefaultImage = document.getElementById('btn-default-image');
const image = document.querySelector('img');
const fileInput = document.querySelector('input[type=file]');
const currentImage = document.getElementById('teacher-image');

btnUpdateImage.addEventListener('click', () => fileInput.click());
btnDefaultImage.addEventListener('click', () => {

	// hide image
	image.style.opacity = '0.0';

	// show image
	setTimeout(() => {
		image.setAttribute('src', currentImage.value);
		image.style.opacity = '1.0';
	}, 500);
});

fileInput.addEventListener('change', (e) => {
	if (e.target.files) {
		let file = e.target.files[0];
		const reader = new FileReader();

		// hide image
		image.style.opacity = '0.0';

		setTimeout(() => {
			reader.onload = function (e) {
				image.setAttribute('src', e.target.result);
			};
			reader.readAsDataURL(file); 

			image.style.opacity = '1.0';
		}, 700);
	}
})

const action = image.dataset.action;
if (action !== undefined && action === 'edit') {
	setTimeout(() => {
		image.style.display = 'block';
	}, 200);

	setTimeout(() => {
		image.style.opacity = '1.0';
	}, 400);
}

 