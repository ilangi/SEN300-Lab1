let id = 0;
let url = `http://localhost:9000/catalog/`




const btn1 = document.getElementById('btn1');
const btn2 = document.getElementById('btn2');
const btn3 = document.getElementById('btn3');
const btn4 = document.getElementById('btn4');


// Grabs a set of 
const getItem = myData => {
    // This guard statement allows the user to cycle through all of the items going in either direction
    if(myData[id] == null)
    {
        // for the prev item
        if(id < 0)
        {
            id = myData.length-1;
        }
        // for the next item
        else
        {
            id = 0;
        }
        
    }
    
    // Item data from the ItemAPI
    myItem = [
        myData[id].id,
        myData[id].title,
        myData[id].details,
        myData[id].unit_price,
        myData[id].quantity
    ]

    return myItem;
}

const searchItem = (myData,title) => {
    
    const parameters = /.*()/gmi;
    const regex = new RegExp(`\\.*(${title})`, 'gmi')
    const matches = []
    for(let i = 0; i < myData.length; i++)
    {
        matches[i] = (myData[i].title).matchAll(regex);
    }
    return matches;
}



const launch = myData => {

    console.log(myData);
    let myItem = [];

    document.getElementById('item').innerHTML = `<h4>Title: ${myData[0].title}</h4> <h5>Details: ${myData[0].details}</h5> <p>Unit Price: $${myData[0].unit_price} <br>Quantity: ${myData[0].quantity}</p>`;
    // determines what button will do what
    const handleClick = evt => {
            switch(evt.target.id)
        {
            case "btn1":
                id++;
                myItem = getItem(myData);
                break;
            case "btn2":
                id--;
                myItem = getItem(myData);
                
                break;
            case 'btn3':
                console.log(searchItem(myData,'paper'))
                break;
            case 'btn4':
                answer = shuffledAnswers[3][1];
                break;
        }
        
          //  document.getElementById('item').innerHTML = `ID: ${myItem[0]}  Title: ${myItem[1]} Details: ${myItem[2]} Unit Price: ${myItem[3]} Quantity: ${myItem[4]}`;
        document.getElementById('item').innerHTML = `<h4>Title: ${myItem[1]}</h4> <h5>Details: ${myItem[2]}</h5> <p>Unit Price: $${myItem[3]} <br>Quantity: ${myItem[4]}</p>`;
        };
        
    

    btn1.addEventListener('click',handleClick);
    btn2.addEventListener('click',handleClick);
    btn3.addEventListener('click',handleClick);
    btn4.addEventListener('click',handleClick);
};


fetch(url) // fetch happens asynchronisly it takes more times than other things so this allows it to be completed and used without error

    .then(response => response.json())
    .then(data => {
        launch(data);
        
    });