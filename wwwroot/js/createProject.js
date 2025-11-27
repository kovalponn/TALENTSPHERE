let countBlock = 0;

const addUserButton = document.getElementById("addUser");
const deleteUserButton = document.getElementById("deleteUser");
const targetBlock = document.getElementById('targetBlock');
const template = document.getElementById('template');

addUserButton.addEventListener('click', function() {
    if(countBlock == 5) {
        alert('Нельзя добавлять пользователей больше пяти');
        return;
    }
    countBlock += 1;
    const hasAdded = document.getElementById(countBlock);
    hasAdded.style.display = 'block';
});

deleteUserButton.addEventListener('click', function() {
    const hasDeleted = document.getElementById(countBlock);
    if (hasDeleted) {
        hasDeleted.style.display = 'none';
        countBlock -= 1; 
    }
});