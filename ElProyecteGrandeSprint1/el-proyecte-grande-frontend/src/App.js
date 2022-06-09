import React, { Component, useEffect, useCallback } from 'react';
import { useState } from "react";
import Container from './components/Container';
import Header from './components/Header';
import News from './components/News';
import Deals from './components/Deals';
import Home from "./components/Home";
import {wait} from "@testing-library/user-event/dist/utils";

let slideIndex = 0;
let timeout = '';
function App() {
    const [text, setText] = useState('');

    const loadNews = useCallback( () => {
        fetchData(`https://localhost:7064/News/`)
            .then(data => {
            setText(<News fetchData={data} showGameNews={showGameNews} showLatestNews={loadNews} searchedNews={""} />)
            window.localStorage.setItem('state', 'NewsMain');
        })
    }, [])

    useEffect(() => {
        switch(window.localStorage.getItem('state')) {
          case 'NewsMain':
            loadNews();
            console.log("Load Recent News");
            break;
          case 'NewsSpecific':
            reloadSpecificGameNews();
            break;
          case 'Deals':
            loadDeals();
            break;
            case 'home':
                loadHome();
                break;
          default:
            loadHome();
            break;
        }
    }, [])
    
    const reloadSpecificGameNews = () => {
        let searchedNews = window.localStorage.getItem('searchedNews')
            fetchData(`https://localhost:7064/News/${searchedNews}`)
            .then(data => {
                console.log("Load Specific Game News");
                setText(<News fetchData={data} showGameNews={showGameNews} showLatestNews={loadNews} searchedNews={searchedNews} />)
            })
    }

    const loadDeals = () => {
        setText(<Deals fetchData={fetchData} showDeals={showGameNews} />)
        window.localStorage.setItem('state', 'Deals');
    }

    const loadHome = () => {
        setText(<Home fetchData={fetchData} />)
        wait(2000).then(x => {showSlides()})
        window.localStorage.setItem('state', 'Home');
    }

    const showGameNews = useCallback((event) => {
        const searchedNews = event.target.textContent;
        fetchData(`https://localhost:7064/News/${searchedNews}`)
            .then(data => {
                setText(<News fetchData={data} showGameNews={showGameNews} showLatestNews={loadNews} searchedNews={searchedNews} />)
                window.localStorage.setItem('state', 'NewsSpecific');
                window.localStorage.setItem('searchedNews', searchedNews)
            })
    }, [])

    return (
        <div className='bg-dark text-white'>
            <Header loadNews={loadNews} loadDeals={loadDeals} loadHome={loadHome}/>
            <Container text={text} />
        </div>
    );
}

async function fetchData(url) {
    const response = await fetch(url);
    if (response.ok){
        const data = await response.json();
        return data;
    }
    throw response;
}


function showSlides() {
    if(timeout)
        stopTimeout(timeout);
    let i;
    let slides = document.getElementsByClassName("mySlides");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    slides[slideIndex - 1].style.display = "block";
    timeout = setTimeout(showSlides, 5000); // Change image every 2 seconds
}
function stopTimeout(timeout) {
    clearTimeout(timeout);
}

export default App;