


document.querySelectorAll('.dropdown-btn').forEach(button => {
    button.addEventListener('click', () => {
        const dropdownContent = button.nextElementSibling;
        dropdownContent.style.display = dropdownContent.style.display === 'inline-block' ? 'none' : 'inline-block';
    });
});
function openNav() {
    
    document.getElementById("mySidebar").style.width = "250px";

}

function closeNav() {
    document.getElementById("mySidebar").style.width = "0";
}



function showTabs() {
    const tabs = document.getElementById("profileTabs");

    if (tabs.style.display == "none" || tabs.style.display == "") {
        tabs.style.display = "flex";
    }
    else {
        tabs.style.display = "none";
    }
    

    alarm("asd");

}



function loadQuizzes(section) {
    window.location.href = `/User/Quiz/Index?section=${section}`;
    ViewBag.Section = section;
}

function loadLearningMaterials(section) {
    window.location.href = `/User/LearningMaterials/Index?section=${section}`;
    ViewBag.Section = section;
}

function loadExampleTasks(section) {
    window.location.href = `/User/ExampleTask/Index?section=${section}`;
    ViewBag.Section = section;
}