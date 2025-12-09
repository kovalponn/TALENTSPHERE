let countBlock = 0;
let countStage = 1;

const nextStageButton = document.getElementById("nextButton");
const backStageButton = document.getElementById("backButton");
const fisrtStageBlock = document.getElementById("step1");
const secondStageBlock = document.getElementById("step2");
const thirdStageBlock = document.getElementById("step3");

secondStageBlock.style.display = 'none';
thirdStageBlock.style.display = 'none';


nextStageButton.addEventListener('click', function() {
    if (countStage == 3) {
        return;
    }

    const presentBlock = document.getElementById("step" + countStage);
    presentBlock.style.display = 'none';

    countStage += 1;
    
    if (countStage == 3) {
        nextStageButton.style.display = 'none';
    }

    const nextBlock = document.getElementById("step" + countStage);
    nextBlock.style.display = 'block';
});

backStageButton.addEventListener('click', function() {
    if (countStage == 1) {
        return;
    }

    const presentBlock = document.getElementById("step" + countStage);
    presentBlock.style.display = 'none';

    countStage -= 1;
    
    if (countStage != 3) {
        nextStageButton.style.display = 'inline';
    }

    const nextBlock = document.getElementById("step" + countStage);
    nextBlock.style.display = 'block';
});

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