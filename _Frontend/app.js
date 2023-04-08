async function GetData(url) {
    const response = await fetch(url);
    var data = await response.json();
    console.log(data);
    showData(data);
    return data;
}

getExpenses = GetData('https://localhost:44375/api/expenses/');
// Categories = GetData('https://localhost:44375/api//');

async function showData(data) {
    let tab =
    `<tr>
    <th>Id</th>
    <th>Name</th>
    <th>Person.Username</th>
    <th>CategoryName</th>
    <th>Cost</th>
    <th>Date</th>
    <th></th>
    </tr>`;
    
    // Loop to access all rows 
    for (let index = 0; index < data.length; index++) {
        tab += `<tr> 
        <td>${data[index].Id} </td>
        <td>${data[index].Name}</td>
        <td>${data[index].Person.Username}</td> 
        <td>${data[index].Category.Name}</td>          
        <td>${data[index].Cost}</td>          
        <td>${data[index].Date}</td>     
        <td><a href='https://localhost:44375/api/expense/${data[index].Id}'>details</a></td>
    </tr>`;
    }
    // Setting innerHTML as tab variable
    document.getElementById("employees").innerHTML = tab;
}