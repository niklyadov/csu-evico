# ==== CONFIGURE =====
FROM node:16-alpine 
WORKDIR /app
COPY ./Evico.Web/ClientApp/ .
# ==== BUILD =====
RUN npm ci --silent
RUN npm run build
# ==== RUN =======
#ENV NODE_ENV production
EXPOSE 3000
CMD [ "npx", "serve", "build" ]