const loginBtn = document.querySelector(".login .title")
const login = document.querySelector(".login")
const signupBtn = document.querySelector('.signup .title')
const signup = document.querySelector(".signup")
loginBtn.addEventListener('click', () => {
    login.classList.toggle("slide-up")
    signup.classList.toggle("slide-up")
});

signupBtn.addEventListener('click', () => {
    login.classList.toggle("slide-up")
    signup.classList.toggle("slide-up")
})