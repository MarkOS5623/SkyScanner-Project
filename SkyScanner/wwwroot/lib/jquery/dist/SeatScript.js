const container = document.querySelector('.container');
const banana = document.querySelector('.planediv');
const seats = document.querySelectorAll('.row .seat:not(.occupied'); //will only get un occupied seats
const count = document.getElementById('count'); //number of selected seats
const total = document.getElementById('total'); //total price of seats
const price = document.getElementById('Price');
const FlightID = document.getElementById('FlightId');
localStorage.setItem('selectedFlightID', FlightID.value);
console.log(localStorage.getItem('selectedFlightID'));
const seatnumtotal = document.getElementById('NumberOfSeats');

const str = BookedSeats.value;
const BookedIndexes = str.split(',').map(Number);
let popped = BookedIndexes.pop();
console.log(popped);

populateUI();

let SeatPrice = Price.value;

// update total and count

function updateSelectedCount() {

    const selectedSeats = document.querySelectorAll('.row .seat.selected');

    const seatsIndex = [...selectedSeats].map((seat) => [...seats].indexOf(seat));

    const ids = [...selectedSeats].map(x => x.value);
    console.log(ids);
    localStorage.setItem('selectedSeats', JSON.stringify(seatsIndex));
    localStorage.setItem('SeatValues', JSON.stringify(ids));
    //copy selected seats into arr
    //map through array
    //return new array of indexes

    const selectedSeatsCount = selectedSeats.length;

    count.innerText = seatsIndex;
    console.log(seatsIndex);
    total.innerText = selectedSeatsCount * SeatPrice;
}

// get data from localstorage and populate ui
function populateUI() {
    const selectedSeats = JSON.parse(localStorage.getItem('selectedSeats'));
    if (selectedSeats !== null && selectedSeats.length > 0) {
        seats.forEach((seat, index) => {
            if (selectedSeats.indexOf(index) > -1) {
                seat.classList.add('selected');
            }
        });
    }
    if (BookedIndexes !== null && BookedIndexes.length > 0) {
        seats.forEach((seat, index) => {
            if (BookedIndexes.indexOf(index) > -1) {
                seat.classList.add('occupied');
            }
        });
    }
}

function DepopulateUI() {
    const selectedSeats = JSON.parse(localStorage.getItem('selectedSeats'));
    if (selectedSeats !== null && selectedSeats.length > 0) {
        seats.forEach((seat, index) => {
            if (selectedSeats.indexOf(index) > -1) {
                seat.classList.add('seat');
            }
        });
    }
    if (BookedIndexes !== null && BookedIndexes.length > 0) {
        seats.forEach((seat, index) => {
            if (BookedIndexes.indexOf(index) > -1) {
                seat.classList.add('seat');
            }
        });
    }
}

banana.addEventListener('click', (E) => {
    E.preventDefault();
    $.ajax({
        type: "POST",
        dataType: "json; charset=utf-8",
        url: "/Flight/BookSeat",
        data: {
            Seats: JSON.parse(localStorage.getItem('SeatValues')),
            Indexes: JSON.parse(localStorage.getItem('selectedSeats')),
            FlightID: JSON.parse(localStorage.getItem('selectedFlightID'))
           }
        });
    DepopulateUI();
    localStorage.clear();
    window.location = "/Flight/FlightList", true;
});

// Seat click event
container.addEventListener('click', (e) => {
    if (e.target.classList.contains('seat') && !e.target.classList.contains('occupied')) {
        e.target.classList.toggle('selected');
        updateSelectedCount();
    }
});

// intial count and total
updateSelectedCount();