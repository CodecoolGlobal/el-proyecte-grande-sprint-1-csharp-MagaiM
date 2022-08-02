FROM node

# set working directory
WORKDIR /app

#COPY . .
# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY ElProyecteGrandeSprint1/el-proyecte-grande-frontend/package.json /app/package.json
COPY ElProyecteGrandeSprint1/el-proyecte-grande-frontend/package-lock.json /app/package-lock.json
RUN npm install
RUN npm install react-scripts -g

# add app
COPY . ./

EXPOSE 3000

# start app
CMD ["npm", "start"]