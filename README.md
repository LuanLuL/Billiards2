# Desenvolvimento de um Jogo 3D de Sinuca

## Descrição do Projeto

Este projeto visa o desenvolvimento de um **jogo tridimensional de sinuca**, utilizando a engine **Unity**, com o objetivo de aplicar os conceitos de **Computação Gráfica** estudados ao longo da disciplina. O jogo simula um ambiente de sinuca, com bolas, tacos e mesas 3D, e implementa física realista de colisões, interação com o usuário e regras do jogo.

## Funcionalidades

- **Jogo de Sinuca 3D** com bolas, tacos e mesa modelados.
- **Simulação física realista** para movimentação das bolas e colisões.
- **Controle de turnos** para dois jogadores.
- **Câmera interativa** para direcionamento da tacada e visualização do jogo.
- **Interface gráfica** com contagem de bolas restantes e informações sobre o turno do jogador.

## Tecnologias Utilizadas

- **Unity 3D**: Engine de desenvolvimento de jogos 3D.
- **C\#**: Linguagem de programação utilizada para os scripts.
- **Modelos 3D** adquiridos no [CGTrader](https://www.cgtrader.com/3d-models/furniture/furniture-set/snooker-room).

## Como Rodar o Projeto

### Pré-requisitos

- **Unity 2025** ou versão superior instalada.
- **C\#** para scripts.

### Passos para execução

1. Clone o repositório:
    ```bash
    git clone https://github.com/usuario/repositorio.git
    ```
2. Abra o projeto na Unity.
3. Clique em "Play" para rodar o jogo.

## Estrutura do Repositório

- `Assets/`: Pasta com todos os arquivos de recursos (modelos, texturas, materiais, etc).
- `Scripts/`: Contém os scripts em C# que implementam a lógica do jogo (como `GameManager`, `Ball`, `CameraController`).
- `Scenes/`: Contém as cenas do jogo.

## Desafios Enfrentados

- **Posicionamento dos objetos**: Alinhar corretamente todos os elementos da cena foi um desafio. O processo de ajuste e testes constantes foi essencial para garantir que tudo funcionasse corretamente.
- **Configuração da física**: Ajustar os parâmetros da física das bolas, como massa, força e atrito, foi uma das partes mais desafiadoras, pois pequenas variações impactavam a jogabilidade.

## Conclusão

Este projeto permitiu a aplicação prática dos conceitos de **Computação Gráfica** em um jogo 3D, envolvendo modelagem, simulação física e interação com o usuário. O desenvolvimento foi focado na construção de um ambiente virtual realista e funcional, proporcionando uma experiência prática e imersiva.
