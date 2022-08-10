FROM openjdk:11-jdk-slim-buster
COPY target/Microservice-ActiveDirectory-0.0.1.jar micro-ad-1.0.0.jar
ENV TZ=America/Guayaquil
ENTRYPOINT ["java","-jar","/micro-ad-1.0.0.jar"]