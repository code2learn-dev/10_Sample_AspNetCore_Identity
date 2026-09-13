const addBtnImage = document.getElementById("btn-add-image");
const removeBtnImage = document.getElementById("btn-remove-image");
const fileInput = document.querySelector('input[type=file]');
const image = document.querySelector('img');
const currentImageUrl = document.getElementById('course-image-url');

const action = image.dataset.action;
if (action !== undefined && action === 'edit') {
	setTimeout(() => {
		image.style.display = 'block';
	}, 200);
	setTimeout(() => {
		image.style.opacity = '1.0';
	}, 400);
};

addBtnImage.addEventListener("click", () => fileInput.click());
fileInput.addEventListener('change', (e) => {
	if (e.target.files[0]) {
		const file = e.target.files[0];
		const reader = new FileReader();

		image.style.opacity = '0.0';
		setTimeout(() => {
			reader.onload = function (e) {
				image.setAttribute('src', e.target.result);
			};
			reader.readAsDataURL(file);
		}, 400);

		setTimeout(() => {
			image.style.opacity = '1.0';
		}, 800);
	}
});

removeBtnImage.addEventListener('click', () => {
	fileInput.value = '';

	image.style.opacity = '0.0';
	setTimeout(() => {
		image.setAttribute('src', currentImageUrl.value);
	}, 400);
	setTimeout(() => {
		image.style.opacity = '1.0';
	}, 500);
});
 