FROM nginx:latest

COPY ./default.conf /etc/nginx/conf.d/default.conf

COPY ./src/index.html /usr/share/nginx/html/index.html
COPY ./src/js /usr/share/nginx/html/js

EXPOSE 80
