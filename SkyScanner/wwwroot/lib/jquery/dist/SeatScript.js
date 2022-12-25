const container = document.querySelector('.container');
const banana = document.querySelector('.planediv');
const seats = document.querySelectorAll('.row .seat:not(.occupied'); //will only get un occupied seats
const count = document.getElementById('count'); //number of selected seats
const total = document.getElementById('total'); //total price of seats
const price = document.getElementById('Price');
const FlightId = document.getElementById('FlightId');
const seatnumtotal = document.getElementById('NumberOfSeats');

populateUI();

let SeatPrice = Price.value;

// Save selected movie index and price
function setBookingData(FlightID, SeatPrice) {
    localStorage.setItem('selectedFlightID', FlightID);
    localStorage.setItem('selectedSeatPrice', SeatPrice);
}

// update total and count
function updateSelectedCount() {

    const selectedSeats = document.querySelectorAll('.row .seat.selected');

    const seatsIndex = [...selectedSeats].map((seat) => [...seats].indexOf(seat));
    localStorage.setItem('selectedSeats', JSON.stringify(seatsIndex));
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

    const selectedFlightID = localStorage.getItem('selectedFlightID');

    if (selectedFlightID !== null) {
        selectedFlightID.selectedIndex = selectedFlightID;
    }
}
banana.addEventListener('click', (E) => {
    E.preventDefault();
    $.ajax({
        type: "Post",
        dataType: "json",
        url: "/Flight/BookSeat",
        data: {
            Pog: JSON.parse(localStorage.getItem('selectedSeats'))
        }
    });
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