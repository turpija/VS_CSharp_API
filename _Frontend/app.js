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
        <td><input type="text" name="row1" id="" value=${data[index].Id} disabled></td>
        <td><input type="text" name="row1" id="" value=${data[index].Name} disabled></td>
        <td><input type="text" name="row1" id="" value=${data[index].Person.Username} disabled></td> 
        <td><input type="text" name="row1" id="" value=${data[index].Category.Name} disabled></td>          
        <td><input type="numer" name="row1" id="" value=${data[index].Cost} disabled></td>          
        <td><input type="string" name="row1" id="" value=${data[index].Date} disabled></td>   
        <td><a href='https://localhost:44375/api/expense/${data[index].Id}'>details</a></td>
    </tr>`;
    }
    // Setting innerHTML as tab variable
    document.getElementById("employees").innerHTML = tab;
}