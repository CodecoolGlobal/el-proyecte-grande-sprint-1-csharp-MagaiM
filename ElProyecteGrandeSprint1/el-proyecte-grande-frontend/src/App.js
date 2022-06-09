import React, { Component, useEffect, useCallback } from 'react';
import { useState } from "react";
import Container from './components/Container';
import Header from './components/Header';
import News from './components/News';

function App(){

    const [text , setText] = useState("");
    const [data, setData] = useState([]);

    useEffect(() => {
        fetchData('https://localhost:7064/News/wtf').then(fetchedData => {
            setData(fetchedData)
        })
    }, [])

    const loadNews = useCallback( () => {
        fetchData(`https://localhost:7064/News/`)
            .then(data => {
            setText(<News fetchData={data} showGameNews={showGameNews} showLatestNews={loadNews} searchedNews={""} />)
        })
    }, [])

    const showGameNews = useCallback((event) => {
        const searchedNews = event.target.textContent;
        fetchData(`https://localhost:7064/News/${searchedNews}`)
            .then(data => {
            setText(<News fetchData={data} showGameNews={showGameNews} showLatestNews={loadNews} searchedNews={searchedNews} />)
        })
    }, [])

    return (
        <div className='bg-dark text-white'>
            <Header loadNews={loadNews} />
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

export default App;