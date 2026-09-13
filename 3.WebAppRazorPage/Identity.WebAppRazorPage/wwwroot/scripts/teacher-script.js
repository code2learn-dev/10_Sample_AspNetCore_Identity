const btnAddImage = document.getElementById('btn-add-image');
const btnRemoveImage = document.getElementById('btn-remove-image');
const fileInput = document.querySelector('input[type=file]');
const image = document.querySelector('img');
const imageText = document.querySelector('section.image-container p');

btnAddImage.addEventListener('click', () => fileInput.click());
btnRemoveImage.addEventListener('click', () => {

	image.style.opacity = '0.0';
	setTimeout(() => {
		image.style.display = 'none';
		imageText.style.opacity = '1.0';
	}, 400); 

	fileInput.value = '';
});

fileInput.addEventListener('change', (e) => {
	if (e.target.files) {  
		let file = e.target.files[0];
		const reader = new FileReader();
		reader.onload = function (e) {
			image.setAttribute('src', e.target.result);
		};
		reader.readAsDataURL(file);

		imageText.style.opacity = '0.0';
		setTimeout(() => {
			image.style.display = 'block';
		}, 200);
		setTimeout(() => {
			image.style.opacity = '1.0';
		}, 400);
	}
});
 
const action = image.dataset.action;
if (action !== undefined && action === 'edit') {
	image.style.display = 'block';
	setTimeout(() => {
		image.style.opacity = '1.0';
	}, 400);
}
