
var numSelected = null
var tileSelected = null

var errors = 0



function selectNumber(id)
{
    if (numSelected != null)
    {
        numSelected.classList.remove("number-selected")
    }
    numSelected = document.getElementById(id);
    numSelected.classList.add("number-selected");
}

function selectTile(id, solution)
{
    let element = document.getElementById(id);
    if (numSelected)
    {
        if (element.innerText != "")
        {
            return;
        }
       

        let coords = element.id.split("-");
        let r = parseInt(coords[0]);
        let c = parseInt(coords[1]);

        if (solution[r][c] == numSelected.id) {
            element.innerText = numSelected.id;
        }
        else {
            errors += 1;
            document.getElementById("errors").innerText = errors;

        }
    }
}

function resetError()
{
    document.getElementById("errors").innerText = 0;
}