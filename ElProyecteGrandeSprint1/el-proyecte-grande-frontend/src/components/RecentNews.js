import React from 'react'
import RecentNewsCard from './RecentNewsCard'

const RecentNews = ({newsData}) => {
    
    return (
        <div className="recent-news">
            <h3 className='recent-news-title'>Recent news</h3><br/>
            {newsData.map((news, index) => (
                <div key={index} className="recent-news-card" >
                    <RecentNewsCard news={news}/>
                    <br/>
                </div>
            ))}
        </div>
    )
}

export default RecentNews