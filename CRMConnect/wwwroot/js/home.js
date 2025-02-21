document.addEventListener("DOMContentLoaded", function() {
    fetchData("customers", "customerList");
    fetchData("users", "userList");
    fetchData("logs", "logList");
    fetchData("tasks", "taskList");
    fetchData("sales", "salesList");
});

function fetchData(endpoint, listId) {
    fetch(`http://localhost:5146/api/${endpoint}`)
        .then(response => response.json())
        .then(data => {
            const list = document.getElementById(listId);
            list.innerHTML = "";
            data.forEach(item => {
                const li = document.createElement("li");
                li.className = "list-group-item";
                li.textContent = Object.values(item).join(" - ");
                list.appendChild(li);
            });
        })
        .catch(error => console.error("Error fetching data:", error));
}

function showTab(tabId) {
    document.querySelectorAll(".tab-content").forEach(tab => {
        tab.classList.add("d-none");
    });
    document.getElementById(tabId).classList.remove("d-none");
}