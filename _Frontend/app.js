async function funcName(url) {
    const response = await fetch(url);
    var data = await response.json();
    console.log(data);
}

funcName('https://localhost:44375/api/expenses/');