import RecentNewsCard from '../components/RecentNewsCard'
import React, { useEffect, useState } from 'react';

const RecentNews = () => {

    const [newsData, setNewsData] = useState([]);
    const baseurl = 'https://localhost:44321/';

    useEffect(() => {
        fetchData(`${baseurl}News/`)
            .then(data=>{setNewsData(data)})
    }, [])

    const formatDate = (timestamp) => {
        const x = new Date(timestamp);
        const dd = x.getDate();
        const mm = x.getMonth() + 1;
        const yy = x.getFullYear();
        return dd + " / " + mm + " / " + yy;
    }

    return (
        <div className="recent-news">
            <h3 className='recent-news-title'>Recent news</h3><br/>
            {newsData.map((news, index) => {
                let date = new Date(news.Date);
                let formattedDate = formatDate(date);
                return(
                <div key={index} className="recent-news-card" >
                    <RecentNewsCard news={news} formattedDate={formattedDate}/>
                    <br/>
                </div>
            )})}
        </div>
    )
}

async function fetchData(url) {
    const response = await fetch(url);
    if (response.ok){
        const data = await response.json();
        return data;
    }
    throw response;
}

export default RecentNews